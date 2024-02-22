using LabyrinthExplorer.Data.Repositories;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Logic.Loggers;
using LabyrinthExplorer.Logic.Models.GameElements;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;



namespace LabyrinthExplorer.Logic
{
    public class GameEngine
    {
        private IGlobalRepository repository = new GlobalRepository();
        public Logger logger = new Logger();

        public UserPlayer UserPlayer { get; set; } = new UserPlayer();
        public NPCPlayer NPCPlayer { get; set; } = new NPCPlayer();
        public Level Level { get; set; } = new Level();
        public List<ItemElement> Inventory { get; set; }
        public GameElement[][] Map { get; set; }
        public char[][] Canvas { get; set; }


        public GameEngine(string levelName = "test")
        {
            logger.Log("START: GameEngine Contructor");
            Level = LoadLevel(levelName);
            Canvas = DeepCopyCanvas(Level.Map);
            Map = ParseCanvasToMap(Canvas);

            logger.Log("END: GameEngine Contructor");
        }

        public Level LoadLevel(string levelName)
        {
            Level output = repository.GetLevel(levelName);
            if (output.DTO.Success == true)
            {
                logger.Log($"LoadLevel: Loaded Level ${levelName}");
                return output;
            }
            else
            {
                logger.LogError($"LoadLevel: Can't load level ${levelName} from Repository");
                return new Level();
            }
        }

        public GameElement[][] ParseCanvasToMap(char[][] canvas)
        {
            GameElement[][] output = new GameElement[canvas.Length][];

            uint counter = 0;

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
                            output[i][j] = new UserPlayer(i, j);
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

            logger.Log($"ParseCanvasToMap: Parsed Canvas to Map of ${counter} elements.");
            return output;
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

            return output;
        }

    }
}
