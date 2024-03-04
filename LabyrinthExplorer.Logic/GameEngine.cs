using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.Repositories;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Loggers;
using LabyrinthExplorer.Logic.Models;
using LabyrinthExplorer.Logic.Models.GameElements;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;



namespace LabyrinthExplorer.Logic
{
    public class GameEngine
    {
        private IGlobalRepository repository = new GlobalRepository(); //przenieś do konstruktora
        public Logger logger = new Logger();

        public UserPlayer? UserPlayer { get; set; } = new UserPlayer();
        public List<NPCPlayer> NPCPlayer { get; set; } = new List<NPCPlayer>();
        public Level Level { get; set; } = new Level();
        public List<ItemElement> Inventory { get; set; } = new List<ItemElement>();
        public GameElement[][] Map { get; set; }
        public char[][] Canvas { get; set; }
        public InputAction InputAction{ get; set; }
        public InterActionDTO UserPlayerInterActionDTO { get; set; } = new InterActionDTO();
        public InterActionDTO UserPlayerReturnedInterActionDTO { get; set; } = new InterActionDTO();
        public GameEngineOutputDTO GameEngineOutputDTO { get; set; } = new GameEngineOutputDTO();


        public GameEngine(string levelName, char[][]? injectedLevelCanvas = null)
        {
            logger.Log("START: GameEngine Contructor");

            if (injectedLevelCanvas == null)
                repository = new GlobalRepository();
            else
                repository = new GlobalRepository(injectedLevelCanvas);

            Level = LoadLevel(levelName);
            Canvas = DeepCopyCanvas(Level.Map);
            Map = ParseCanvasToMap(Canvas);
            if ((UserPlayer = InitializeUserPlayer(Map)) == null)
            {
                logger.LogError("GameEngine Constructor: Can't initialize UserPlayer. Exiting the Game Initialization.");
                return;
            }
            
            NPCPlayer.AddRange(InitializeNPCPlayers(Map));
            Inventory.AddRange(InitializeInventory(Map));

            logger.Log("END: GameEngine Contructor");
        }
        public GameEngineOutputDTO RunEngine(GameEngineInputDTO input)
        {
            //1.ReceiveInputDTO
            //2.Translate InputDTO to InterActionDTO
            //3. Send InterActionDTO to HumanPlayer
            //4. Let Player Do his black Box
            InputAction = ReceiveInputDTO(input);
            UserPlayerInterActionDTO = TranslateInputActionToInterAction(InputAction, UserPlayer.Position);
            UserPlayerReturnedInterActionDTO = UserPlayer.ReceiveInterActionDTO(UserPlayerInterActionDTO);
            Map = ApplyInterActionDTOOnGameElementMap(Map, UserPlayerReturnedInterActionDTO);
            GameEngineOutputDTO = PrepareGameEngineOutputDTO(Map, logger);

            return GameEngineOutputDTO; //not implemented
        }

        public Level LoadLevel(string levelName)
        {
            Level output = repository.GetLevel(levelName);
            if (output.DTO.Success == true)
            {
                logger.Log($"LoadLevel: Loaded Level {levelName}");
                return output;
            }
            else
            {
                logger.LogError($"LoadLevel: Can't load level {levelName} from Repository");
                return new Level();
            }
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
                            if(foundUserPlayer == true) //if there is a second User Player
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
                        case 'D':
                            output[i][j] = new Door(i, j);
                            break;
                        case 'O':
                            output[i][j] = new Door(i, j) { Open = true };
                            break;
                        case 'L':
                            output[i][j] = new Door(i, j) { Locked = true };                            
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
                    if (mapOfElements[i][j] is NPCPlayer)
                    {
                        logger.Log($"InitializeNPCPlayer: Found NPCPlayer {counter} at ({i},{j})");
                        npcPlayers.Add(new NPCPlayer()
                        {
                            Name = $"NPC Player {counter++}"
                        });
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
        public InputAction ReceiveInputDTO(GameEngineInputDTO input) 
        {
            if (input.DTO.Success == true)
            {
                logger.Log($"ReceiveInputDTO: Received InputAction: {InputAction.ToString()}");
                return input.InputAction;
            }
            else
            {
                logger.LogError($"ReceiveInputDTO: Error from UI: InputAction: {InputAction.ToString()}");
                return InputAction.Unknown;
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
        public GameEngineOutputDTO PrepareGameEngineOutputDTO(GameElement[][] mapOfElements, Logger logger)
        {
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

            //Set Log
            output.Log = logger.Message.ToString();

            logger.Log($"PrepareGameEngineOutputDTO: Success.");
            return output;
        }
        public GameElement[][] ApplyInterActionDTOOnGameElementMap(GameElement[][] elementMap, InterActionDTO input)
        {
            logger.Log(input.DTO.Message); // Add to log UserPlayer Message
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
    }

}
