using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Data.Repositories;
using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.Loggers;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;
using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyrinthExplorer.Logic.InternalCommunication;
using LabyrinthExplorer.Logic.DTOs;
using System.Data.Common;

namespace LabyrinthExplorer.Logic.Managers.MenuManager
{
    internal class LevelManager : Manager
    {
        
        public Logger logger = new Logger();
        

        public LevelManager()
        {
           
        }


        public override InternalDTO ReceiveInternalDTO(InternalDTO inputDTO)
        {
            if (inputDTO.Event == Event.LevelCheckNextLevel)
            {
                if (IsItLastLevel(inputDTO.CurrentLevelIndex, inputDTO.Levels))
                {
                    inputDTO.Event = Event.MenuGameSummary;
                    inputDTO.Logger.Log($"LevelManager: {Event.LevelLoadNext}, this was the last level, issued {inputDTO.Event}");
                    return inputDTO;
                }
                else
                {
                    inputDTO.Event = Event.LevelLoadNext;
                    inputDTO.Logger.Log($"LevelManager: {Event.LevelLoadNext}, able to load next level, issued {inputDTO.Event}");
                    return inputDTO;
                }
            }
            if (inputDTO.Event == Event.LevelLoadNext)
            {
                inputDTO = InitializeNewGame(inputDTO);
                inputDTO.Event = Event.GameStep;
                inputDTO.Logger.Log($"LevelManager: {Event.LevelLoadNext}, InitializedNewGame, issued {inputDTO.Event}");
                return inputDTO;
            }
            if (inputDTO.Event == Event.LevelNewGame)
            {
                inputDTO.Repository = InitializeRepository(inputDTO.injectedLevelCanvas);
                inputDTO.Levels = InitializeLevelsFromRepository(inputDTO.Repository, inputDTO.LevelName);
                inputDTO.CurrentLevelIndex = 0;
                inputDTO.Event = Event.LevelLoadNext; //TODO change to check next
                inputDTO.Logger.Log($"LevelManager: Received {Event.LevelNewGame} Initialized Repository, Initialized Levels List, issued {inputDTO.Event}");
                return inputDTO;
            }
            if (inputDTO.Event == Event.LevelRestartCurrentLevel)
            {
                if(inputDTO.CurrentLevelIndex != 0) inputDTO.CurrentLevelIndex--;
                inputDTO = InitializeNewGame(inputDTO);
                inputDTO.Event = Event.GameStep;
                inputDTO.Logger.Log($"LevelManager: {Event.LevelLoadNext}, InitializedNewGame, issued {inputDTO.Event}");
                return inputDTO;
            }
            return inputDTO;
        }



        /*****************************
       * LevelsManager
       * **************************/
        private bool IsItLastLevel(int nextLevelIndex, IEnumerable<object> collection)
        {
            if ((nextLevelIndex) <= (collection.Count() - 1)) return false;
            else return true;
        }
        private InternalDTO InitializeNewGame(InternalDTO input)
        {
            input.Logger.Log($"InitializeNewGame: Started. Level index {input.CurrentLevelIndex}");

            input.Level = LoadLevelFromLevelsList(input.CurrentLevelIndex++, input.Levels);

            input.Canvas = DeepCopyCanvas(input.Level.Map); //create deep copy of char[][] array
            input.Map = ParseCanvasToMap(input.Canvas); //translate char array into GameElement Objects array
            if ((input.UserPlayer = InitializeUserPlayer(input.Map)) == null) //if there is no UserPlayer in provided Array, game exits now
            {
                input.Logger.LogError("NewGame: Can't initialize UserPlayer. Exiting the New Game Initialization.");
                return input;
            }
            input.NPCPlayer.Clear();
            input.NPCPlayer.AddRange(InitializeNPCPlayers(input.Map));
            input.Inventory.Clear();
            input.Inventory.AddRange(InitializeInventory(input.Map));

            input.Logger.Log($"InitializeNewGame: Finished. Level index {input.CurrentLevelIndex - 1}");
            return input;
        }
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

    }
}
