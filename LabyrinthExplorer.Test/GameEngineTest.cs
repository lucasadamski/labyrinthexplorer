using LabyrinthExplorer.Logic;
using LabyrinthExplorer.Logic.Models.GameElements;
using System.ComponentModel;
using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Data.Repositories;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;
using System.Linq;

namespace LabyrinthExplorer.Test
{
    [TestClass]
    public class GameEngineTest
    {
        public IGlobalRepository repository { get; set; } = new GlobalRepository();
        [TestMethod]
        public void LoadLevelTest()
        {
            GameEngine GE = new GameEngine(Settings.TEST_LEVEL);

            Level testLevel = repository.GetLevel(Settings.TEST_LEVEL);


            char[][] map = new char[5][]
                 {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                 };
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Assert.AreEqual(map[i][j], GE.Canvas[i][j]);
                }
            }

        }

        /// <summary>
        /// Creates hardcoded 2D array, and compares each item if it's:
        /// 1) same type
        /// 2) same Coordinates
        /// </summary>
        [TestMethod]
        public void ParseCanvasToMapTest()
        {
            GameEngine GE = new GameEngine(Settings.TEST_LEVEL);
            GameElement[][] map = new GameElement[5][]
                 {
                          new GameElement[5] { new CornerWall(0,0) , new HorizontalWall(0, 1), new HorizontalWall(0, 2), new HorizontalWall(0, 3), new CornerWall(0, 4) }
                        , new GameElement[5] { new VerticalWall(1, 0), new UserPlayer(1, 1), new EmptySpace(1, 2), new EmptySpace(1, 3), new VerticalWall(1, 4) }
                        , new GameElement[5] { new VerticalWall(2, 0), new EmptySpace(2, 1), new EmptySpace(2, 2), new EmptySpace(2, 3), new VerticalWall(2, 4) }
                        , new GameElement[5] { new VerticalWall(3, 0), new EmptySpace(3, 1), new EmptySpace(3, 2), new EmptySpace(3, 3), new VerticalWall(3, 4) }
                        , new GameElement[5] { new CornerWall(4, 0), new HorizontalWall(4, 1), new HorizontalWall(4, 2), new HorizontalWall(4, 3), new CornerWall(4, 4) }
                 };

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Assert.AreEqual(map[i][j].GetType(), GE.Map[i][j].GetType());
                    Assert.AreEqual(map[i][j].Position.X, GE.Map[i][j].Position.X);
                    Assert.AreEqual(map[i][j].Position.Y, GE.Map[i][j].Position.Y);
                }
            }
        }

        [TestMethod]
        public void InitalizeUserPlayerTest()
        {
            //One Player
            char[][] map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };
            
            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            bool result = GE.logger.Message.ToString().Contains("InitializeUserPlayer: Found User Player at (1,1)");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //No Player
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeUserPlayer: Initialized User Player at (1,1)");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //3 Players
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', 'P', ' ', '|' }
                        , new char[5] { '|', ' ', 'P', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("ParseCanvasToMap: Found more than 1 User Player. User Player changed to EmptySpace.");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void InitalizeNPCPlayersTest()
        {
            //One NPC
            char[][] map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'E', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            bool result = GE.logger.Message.ToString().Contains("InitializeNPCPlayer: Found NPCPlayer 1 at (1,1)");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //No NPC
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeNPCPlayer: Added 0 NPC Players.");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //3 NPCs
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'E', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', 'E', ' ', '|' }
                        , new char[5] { '|', ' ', 'E', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeNPCPlayer: Added 3 NPC Players.");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void InitalizeInventoryTest()
        {
            //One Item
            char[][] map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', 'K', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GameEngine GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            bool result = GE.logger.Message.ToString().Contains("InitializeInventory: Found 1 Items");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //No Item
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeInventory: Found 0 Items");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);

            //3 Items
            map = new char[5][]
                {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', 'K', '|' }
                        , new char[5] { '|', ' ', 'W', ' ', '|' }
                        , new char[5] { '|', ' ', 'K', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);

            result = GE.logger.Message.ToString().Contains("InitializeInventory: Found 3 Items");
            Console.WriteLine(GE.logger.Message.ToString());
            Assert.AreEqual(true, result);
        }
    }
}