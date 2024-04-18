using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.Repositories;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.InternalCommunication;
using LabyrinthExplorer.Logic.Loggers;
using LabyrinthExplorer.Logic.Managers;
using LabyrinthExplorer.Logic.Managers.MenuManager;
using LabyrinthExplorer.Logic.Models;
using LabyrinthExplorer.Logic.Models.GameElements;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static LabyrinthExplorer.Data.Helpers.Settings;


[assembly: InternalsVisibleTo("LabyrinthExplorer.Test")]
namespace LabyrinthExplorer.Logic
{
    public class GameEngine
    {
        /****************************************************************************************
         *                                     PROPERTIES & FIELDS
         * **************************************************************************************/
        /*****************************
         * Data
         * **************************/
        private IGlobalRepository repository = new GlobalRepository(); //przenieś do konstruktora

        /****************************
         * GameEngine
         * **************************/
        public Logger logger = new Logger();
        public InterActionDTO UserPlayerInterActionDTO { get; set; } = new InterActionDTO();
        public InterActionDTO UserPlayerReturnedInterActionDTO { get; set; } = new InterActionDTO();

        private Manager menuManager;
        private Manager gameManager;
        private Manager levelManager;
        private Manager uiManager;
        private Manager activeManager;
        private InternalDTO internalDTO;

        /*****************************
        * GameManager
        * **************************/
        public UserPlayer? UserPlayer { get; set; } = new UserPlayer();
        public List<NPCPlayer> NPCPlayer { get; set; } = new List<NPCPlayer>();
        public List<ItemElement> Inventory { get; set; } = new List<ItemElement>();

        /*****************************
         * LevelManager
         * **************************/
        public Level Level { get; set; } = new Level();
        private List<Level> Levels { get; set; } = new List<Level>();
        public GameElement[][] Map { get; set; }
        internal char[][] Canvas { get; set; }
        private int _currentLevelIndex = 0;

        /*****************************
         * UIManager
         * **************************/
        public GameEngineOutputDTO GameEngineOutputDTO { get; set; } = new GameEngineOutputDTO();
        public InputAction InputAction { get; set; }

        /*****************************
         * MenuManager
         * **************************/
        private bool _isLevelFinished = false;


        /****************************************************************************************
         *                                          METHODS
         * **************************************************************************************/

        /*****************************
         * GameEngine
         * **************************/
        public GameEngine(string levelName, char[][]? injectedLevelCanvas = null)
        {
            logger.Log("START: GameEngine Contructor");

            /*repository = InitializeRepository(injectedLevelCanvas);

            Levels = InitializeLevelsFromRepository(repository, levelName);

            InitializeNewGame();*/
            internalDTO = new InternalDTO()
            {
                InputAction = InputAction.Use,
                Event = Event.MenuMainNewGame,                             //TODO should be MainMenu 
                DTO = new DTO(),
                Logger = logger,
                UserPlayer = UserPlayer,
                NPCPlayer = NPCPlayer,
                Map = Map,
                Repository = repository,
                Levels = Levels,
                injectedLevelCanvas = injectedLevelCanvas,
                LevelName = levelName,
                Level = Level,
                Canvas = Canvas

            };


            InitializeManagers();


            logger.Log("END: GameEngine Contructor");
        }

        private void InitializeManagers()
        {
            

            levelManager = new LevelManager();

            internalDTO.Event = Event.LevelNewGame;
            internalDTO = levelManager.ReceiveInternalDTO(internalDTO);

            menuManager = new MenuManager(internalDTO);
            gameManager = new GameManager();
            uiManager = new UIManager();
            return;
        }

        private Manager SwitchActiveManager(Event @event) 
        {
            if (@event == Event.MenuGameSummary) return menuManager;
            else if (@event == Event.MenuMainPaused) return menuManager;
            else if (@event == Event.MenuMainNewGame) return menuManager;
            else if (@event == Event.MenuGameOver) return menuManager;
            else if (@event == Event.MenuLevelSummary) return menuManager;
            else if (@event == Event.LevelCheckNextLevel) return levelManager;
            else if (@event == Event.LevelLoadNext) return levelManager;
            else if (@event == Event.LevelNewGame) return levelManager;
            else if (@event == Event.LevelRestartCurrentLevel) return levelManager;
            else if (@event == Event.GameStep) return gameManager;
            else if (@event == Event.UIQuitGame) return uiManager;
            else
                return activeManager;
        }

        public GameEngineOutputDTO RunEngine(GameEngineInputDTO input)
         {
            internalDTO = ReceiveInputDTO(input, internalDTO);
            //internalDTO.InputAction = InputAction;
            while (internalDTO.RequestUIInput == false)
            {
                activeManager = SwitchActiveManager(internalDTO.Event);
                internalDTO = activeManager.ReceiveInternalDTO(internalDTO);
                //update Game Engine fields from internalDTO
            }



            /*UserPlayerInterActionDTO = TranslateInputActionToInterAction(InputAction, UserPlayer.Position);
            UserPlayerReturnedInterActionDTO = UserPlayer.ReceiveInterActionDTO(UserPlayerInterActionDTO);
            logger.AppendDTOMessage(UserPlayerReturnedInterActionDTO.DTO.Message);

            Map = ApplyInterActionDTOOnGameElementMap(Map, UserPlayerReturnedInterActionDTO);*/


            
            GameEngineOutputDTO = PrepareGameEngineOutputDTO(internalDTO.Map, internalDTO.Logger, internalDTO);
            internalDTO.Logger.ClearMessage();

            return GameEngineOutputDTO;
        }


        /*****************************
         * GameManager
         * **************************/
        private void InitializeNewGame()
        {
            logger.Log($"InitializeNewGame: Started. Level index {_currentLevelIndex}");

            Level = LoadLevelFromLevelsList(_currentLevelIndex++, Levels);

            Canvas = DeepCopyCanvas(Level.Map); //create deep copy of char[][] array
            Map = ParseCanvasToMap(Canvas); //translate char array into GameElement Objects array
            if ((UserPlayer = InitializeUserPlayer(Map)) == null) //if there is no UserPlayer in provided Array, game exits now
            {
                logger.LogError("NewGame: Can't initialize UserPlayer. Exiting the New Game Initialization.");
                return;
            }
            NPCPlayer.Clear();
            NPCPlayer.AddRange(InitializeNPCPlayers(Map));
            Inventory.Clear();
            Inventory.AddRange(InitializeInventory(Map));

            logger.Log($"InitializeNewGame: Finished. Level index {_currentLevelIndex - 1}");
        }
        
        public GameElement[][] ApplyInterActionDTOOnGameElementMap(GameElement[][] elementMap, InterActionDTO input)
        {
            try
            {
                elementMap[input.CenterPosition.X - 1][input.CenterPosition.Y - 1] = input.MapOfElements[0][0];
                elementMap[input.CenterPosition.X - 1][input.CenterPosition.Y] = input.MapOfElements[0][1];
                elementMap[input.CenterPosition.X - 1][input.CenterPosition.Y + 1] = input.MapOfElements[0][2];

                elementMap[input.CenterPosition.X][input.CenterPosition.Y - 1] = input.MapOfElements[1][0];
                elementMap[input.CenterPosition.X][input.CenterPosition.Y] = input.MapOfElements[1][1];
                elementMap[input.CenterPosition.X][input.CenterPosition.Y + 1] = input.MapOfElements[1][2];

                elementMap[input.CenterPosition.X + 1][input.CenterPosition.Y - 1] = input.MapOfElements[2][0];
                elementMap[input.CenterPosition.X + 1][input.CenterPosition.Y] = input.MapOfElements[2][1];
                elementMap[input.CenterPosition.X + 1][input.CenterPosition.Y + 1] = input.MapOfElements[2][2];

                logger.Log($"ApplyInterActionDTOOnGameElementMap: Success, Center Position X:{input.CenterPosition.X} Y:{input.CenterPosition.Y}");
                return elementMap;
            }
            catch (Exception e)
            {
                logger.LogError($"ApplyInterActionDTOOnGameElementMap: Error, Center Position X:{input.CenterPosition.X} Y:{input.CenterPosition.Y}");
                return elementMap;
            }
        }

        
        public InterActionDTO TranslateInputActionToInterAction(InputAction inputAction, Coordinates coordinates)
        {
            InterActionDTO interActionDTO = new InterActionDTO();
            interActionDTO.InputAction = inputAction;
            interActionDTO.CenterPosition = new Coordinates(coordinates.X, coordinates.Y);

            //Generate LocalMapOfElements
            try
            {
                interActionDTO.MapOfElements[0] = new GameElement[3];
                interActionDTO.MapOfElements[0][0] = Map[coordinates.X - 1][coordinates.Y - 1];
                interActionDTO.MapOfElements[0][1] = Map[coordinates.X - 1][coordinates.Y];
                interActionDTO.MapOfElements[0][2] = Map[coordinates.X - 1][coordinates.Y + 1];

                interActionDTO.MapOfElements[1] = new GameElement[3];
                interActionDTO.MapOfElements[1][0] = Map[coordinates.X][coordinates.Y - 1];
                interActionDTO.MapOfElements[1][1] = Map[coordinates.X][coordinates.Y];
                interActionDTO.MapOfElements[1][2] = Map[coordinates.X][coordinates.Y + 1];

                interActionDTO.MapOfElements[2] = new GameElement[3];
                interActionDTO.MapOfElements[2][0] = Map[coordinates.X + 1][coordinates.Y - 1];
                interActionDTO.MapOfElements[2][1] = Map[coordinates.X + 1][coordinates.Y];
                interActionDTO.MapOfElements[2][2] = Map[coordinates.X + 1][coordinates.Y + 1];
            }
            catch (Exception e)
            {
                logger.LogError($"TranslateInputActionToInterAction: Can't convert input {inputAction} and coordinates X:{coordinates.X} Y:{coordinates.Y} " +
                $"to InterActionDTO. Exception: {e.Message}");
                return new InterActionDTO();
            }

            logger.Log($"TranslateInputActionToInterAction: Converted input {inputAction} and coordinates X:{coordinates.X} Y:{coordinates.Y} " +
                $"to InterActionDTO");
            return interActionDTO;
        }
        public void ApplyCheats(string cheats)
        {
            if (cheats.Contains("give_all"))
            {
                UserPlayer.Pickup(new Key());
                UserPlayer.Pickup(new Weapon());
            }
            if (cheats.Contains("restore_health"))
            {
                UserPlayer.Health = 100;
            }
        }


        /*****************************
        * LevelsManager
        * **************************/
        public Level LoadLevelFromLevelsList(int levelIndex, List<Level> levels)
        {
            Level output = new Level();
            try
            {
                output = levels.ElementAt(levelIndex);
            }
            catch (Exception e)
            {
                //GAME IS FINISHED, no more levels
                
                logger.Log($"LoadLevel: Can not load level from Levels. Game finished.");
                return new Level();
            }
            logger.Log($"LoadLevel: Loaded level {output.Name}");
            return output;
        }
        public GameElement[][] ParseCanvasToMap(char[][] canvas)
        {
            GameElement[][] output = new GameElement[canvas.Length][];

            uint counter = 0;
            bool foundUserPlayer = false; //only one UserPlayer allowed

            for (int i = 0; i < canvas.Length; i++)
            {
                output[i] = new GameElement[canvas[i].Length];
                for (int j = 0; j < canvas[i].Length; j++)
                {
                    ++counter;
                    switch (canvas[i][j])
                    {
                        case ' ':
                            output[i][j] = new EmptySpace(i, j);
                            break;
                        case '-':
                            output[i][j] = new HorizontalWall(i, j);
                            break;
                        case '|':
                            output[i][j] = new VerticalWall(i, j);
                            break;
                        case '+':
                            output[i][j] = new CornerWall(i, j);
                            break;
                        case 'P':
                            if (foundUserPlayer == true) //if there is a second User Player
                            {
                                logger.LogError("ParseCanvasToMap: Found more than 1 User Player. User Player changed to EmptySpace.");
                                output[i][j] = new EmptySpace(i, j);
                                break;
                            }
                            output[i][j] = new UserPlayer(i, j);
                            foundUserPlayer = true;
                            break;
                        case 'K':
                            output[i][j] = new Key(i, j);
                            break;
                        case 'D': //closed unlocked doors
                            output[i][j] = new Door(i, j);
                            break;
                        case 'O': //open doors
                            output[i][j] = new Door(i, j, true);
                            break;
                        case 'L': //locked doors
                            output[i][j] = new Door(i, j, false, true);
                            break;
                        case 'X':
                            output[i][j] = new Trap(i, j);
                            break;
                        case 'E':
                            output[i][j] = new NPCPlayer(i, j);
                            break;
                        case 'W':
                            output[i][j] = new Weapon(i, j);
                            break;
                        case Settings.MODEL_FINISH_LEVEL_PORTAL:
                            output[i][j] = new FinishLevelPortal(i, j);
                            break;
                        default:
                            break;
                    }
                }
            }

            logger.Log($"ParseCanvasToMap: Parsed Canvas to Map of {counter} elements.");
            return output;
        }
        public UserPlayer? InitializeUserPlayer(GameElement[][] mapOfElements)
        {
            for (int i = 0; i < mapOfElements.Length; i++)
            {
                for (int j = 0; j < mapOfElements[i].Length; j++)
                {
                    if (mapOfElements[i][j] is UserPlayer)
                    {
                        logger.Log($"InitializeUserPlayer: Found User Player at ({i},{j})");
                        return (UserPlayer)mapOfElements[i][j];
                    }
                }
            }
            logger.Log("InitializeUserPlayer: Can't find UserPlayer in GameElements Map.");
            for (int i = 0; i < mapOfElements.Length; i++)
            {
                for (int j = 0; j < mapOfElements[i].Length; j++)
                {
                    if (mapOfElements[i][j] is EmptySpace)
                    {
                        mapOfElements[i][j] = new UserPlayer(i, j);
                        logger.Log($"InitializeUserPlayer: Initialized User Player at ({i},{j})");
                        return (UserPlayer)mapOfElements[i][j];
                    }
                }
            }
            return null;
        }
        public List<NPCPlayer> InitializeNPCPlayers(GameElement[][] mapOfElements)
        {
            List<NPCPlayer> npcPlayers = new List<NPCPlayer>();
            uint counter = 1;
            for (int i = 0; i < mapOfElements.Length; i++)
            {
                for (int j = 0; j < mapOfElements[i].Length; j++)
                {
                    if (mapOfElements[i][j] is NPCPlayer npc)
                    {
                        logger.Log($"InitializeNPCPlayer: Found NPCPlayer {counter} at ({i},{j})");
                        npc.Name += $" {counter++}";
                        npcPlayers.Add(npc);
                    }
                }
            }
            logger.Log($"InitializeNPCPlayer: Added {counter - 1} NPC Players.");
            return npcPlayers;
        }
        public List<ItemElement> InitializeInventory(GameElement[][] mapOfElements)
        {
            List<ItemElement> inventory = new List<ItemElement>();
            uint counter = 1;
            for (int i = 0; i < mapOfElements.Length; i++)
            {
                for (int j = 0; j < mapOfElements[i].Length; j++)
                {
                    if (mapOfElements[i][j] is ItemElement item)
                    {
                        logger.Log($"InitializeInventory: Found {item.Name} at ({i},{j})");
                        inventory.Add((ItemElement)mapOfElements[i][j]);
                        counter++;
                    }
                }
            }
            logger.Log($"InitializeInventory: Found {counter - 1} Items");
            return inventory;
        }
        private char[][] DeepCopyCanvas(char[][] input)
        {
            int sizeA = input.Length;
            int sizeB = input[0].Length;

            char[][] output = new char[sizeA][];
            for (int i = 0; i < sizeA; i++)
            {
                output[i] = new char[input[0].Length];
                for (int j = 0; j < sizeB; j++)
                {
                    output[i][j] = input[i][j];
                }
            }

            logger.Log($"DeepCopyCanvas: Success, Canvas size: {sizeA}x{sizeB}");
            return output;
        }
        /*****************************
         * Data
         * **************************/
        private GlobalRepository InitializeRepository(char[][]? injectedLevelCanvas)
        {
            //allows optional injecting custom level for testing purposes
            GlobalRepository output;
            if (injectedLevelCanvas == null)
            {
                output = new GlobalRepository();  //normal mode
                logger.Log($"InitializeRepository: Initialized repository in normal mode");
            }
            else
            {
                output = new GlobalRepository(injectedLevelCanvas); //custom level for testing
                logger.Log($"InitializeRepository: Initialized repository from injected level canvas");
            }
            return output;
        }
        private List<Level> InitializeLevelsFromRepository(IGlobalRepository repository, string levelName = null)
        {
            List<Level> output = new List<Level>();

            if (levelName == Settings.ALL_LEVELS) //Setting dependency
            {
                try
                {
                    output = repository.GetAllLevels().ToList();
                }
                catch (Exception e)
                {
                    logger.LogError($"LoadAllLevels: Can not load levels from repository. Exception: {e.Message}");
                    return new List<Level>();
                }
                logger.Log($"LoadAllLevels: Loaded {output.Count()} levels");
                return output;
            }
            else
            {
                try
                {
                    output.Add(repository.GetLevel(levelName));
                }
                catch (Exception e)
                {
                    logger.LogError($"LoadAllLevels: Can not load level \"${levelName}\" from repository. Exception: {e.Message}");
                    return new List<Level>();
                }
                logger.Log($"LoadAllLevels: Loaded level \"${levelName}\" from repository");
                return output;
            }
        }

        /*****************************
         * MenuManager
         * **************************/
        
        /*****************************
         * UIManager
         * **************************/
        internal InternalDTO ReceiveInputDTO(GameEngineInputDTO input, InternalDTO internalDTO)
        {
            //overrides Event if OverridenEvent not null
            internalDTO.Event = input.OverridenEvent ?? internalDTO.Event; 

            if (input.DTO.Success == true)
            {
                logger.Log($"ReceiveInputDTO: Received InputAction: {input.InputAction.ToString()}");
                internalDTO.InputAction = input.InputAction;
                return internalDTO;
            }
            else
            {
                logger.LogError($"ReceiveInputDTO: Error from UI: InputAction: {InputAction.ToString()}");
                internalDTO.InputAction = InputAction.Unknown;
                return internalDTO;
            }
        }
        internal GameEngineOutputDTO PrepareGameEngineOutputDTO(GameElement[][] mapOfElements, Logger logger, InternalDTO internalDTO)
        {
            internalDTO.RequestUIInput = false; //przerób na internal dto map 

            GameEngineOutputDTO output = new GameEngineOutputDTO();

            //Translate GameElements array to Char array
            output.Frame = new char[mapOfElements.Length][];
            for (int i = 0; i < mapOfElements.Length; i++)
            {
                output.Frame[i] = new char[mapOfElements[i].Length];
                for (int j = 0; j < mapOfElements[i].Length; j++)
                {
                    output.Frame[i][j] = mapOfElements[i][j].Model;
                }
            }

            logger.Log($"PrepareGameEngineOutputDTO: Success.");

            if (UserPlayer.NotVisible)
            {
                output.DTO.Success = false;
                output.DTO.Message = Settings.MESSAGE_GAME_OVER;
                logger.Log(Settings.MESSAGE_GAME_OVER);
            }

            //Set Log
            output.Log = logger.Message.ToString();

            //Set HUD
            output.HUD = GenerateHUD(output.Log, internalDTO.UserPlayer);

            //Set Level Finished
            output = SetGEOutputDTOForFinishedLevel(_isLevelFinished, output);

            //Is Menu active?
            output.IsGameActive = !internalDTO.IsMenuActive;

            //Is Application active?
            output.IsApplicationActive = internalDTO.IsApplicationActive;

            //Prepare Menu
            if (internalDTO.IsMenuActive)
            {
                output.Menu.Title = internalDTO.Menu.Title; //change to consts
                output.Menu.Message = internalDTO.Menu.Message;
                output.Menu.ActiveOptionIndex = internalDTO.Menu.ActiveOptionIndex;
                output.Menu.Options.Clear();
                foreach (Event option in internalDTO.Menu.Options)
                {
                    switch (option)
                    {
                        case Event.GameStep:
                            output.Menu.Options.Add(EVENT_GAME_STEP);
                            break;
                        case Event.MenuGameSummary:
                            output.Menu.Options.Add(EVENT_MENU_GAME_SUMMARY);
                            break;
                        case Event.MenuMainPaused:
                            output.Menu.Options.Add(EVENT_MENU_MAIN_PAUSED);
                            break;
                        case Event.MenuMainNewGame:
                            output.Menu.Options.Add(EVENT_MENU_NEW_GAME);
                            break;
                        case Event.MenuLevelSummary:
                            output.Menu.Options.Add(EVENT_LEVEL_SUMMARY);
                            break;
                        case Event.MenuGameOver:
                            output.Menu.Options.Add(EVENT_GAME_OVER);
                            break;
                        case Event.LevelCheckNextLevel:
                            output.Menu.Options.Add(EVENT_LEVEL_CHECK_NEXT_LEVEL);
                            break;
                        case Event.LevelLoadNext:
                            output.Menu.Options.Add(EVENT_LEVEL_LOAD_NEXT);
                            break;
                        case Event.LevelNewGame:
                            output.Menu.Options.Add(EVENT_LEVEL_NEW_GAME);
                            break;
                        case Event.LevelRestartCurrentLevel:
                            output.Menu.Options.Add(EVENT_LEVEL_RESTART_CURRENT_LEVEL);
                            break;
                        case Event.UIQuitGame:
                            output.Menu.Options.Add(EVENT_UI_QUIT_GAME);
                            break;
                    }
                }
            }
           
            return output;
        }
        private GameEngineOutputDTO SetGEOutputDTOForFinishedLevel(bool isLevelFinished, GameEngineOutputDTO input)
        {
            if (isLevelFinished)
            {
                GameEngineOutputDTO output = input;
                output.DTO.Success = false;
                output.DTO.Message = Settings.MESSAGE_LEVEL_FINISHED; // UI Settings dependency
                _isLevelFinished = true;
                output.LevelSummary = $"Level finished!\nLevel name:{Level.Name}\nPlayer name:{UserPlayer.Name}";
                return output;
            }
            return input;
        }
        private string GenerateHUD(string log, CharacterElement player)
        {
            //HUD is 3 sections
            //Health: 100
            //Inventory: Key Weapon
            //Messages: Actions that happened in game

            StringBuilder output = new StringBuilder();
            string temp = "";

            //Generate Health section ***********
            output = output.AppendLine($"Health: {player.Health}");

            //Generate Inventory section ****************
            output = output.Append($"Inventory: ");
            foreach (ItemElement item in player.Inventory)
            {
                output = output.Append($"{item.Name} ");
            }
            output = output.AppendLine();

            //Generate Message section *************
            output = output.AppendLine($"Message: ");
            foreach (string logLine in log.Split('\n'))
            {
                if (logLine.Contains(Settings.LOGGER_ACTION))
                {
                    output.AppendLine((logLine.Substring(logLine.IndexOf(Settings.LOGGER_ACTION) + Settings.LOGGER_ACTION.Length)).Trim());
                }
            }
            return output.ToString();
        }

    }
}
