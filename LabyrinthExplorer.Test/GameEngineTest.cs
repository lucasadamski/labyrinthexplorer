using LabyrinthExplorer.Logic;
using LabyrinthExplorer.Logic.Models.GameElements;
using System.ComponentModel;
using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Data.Repositories;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;

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
                        , new GameElement[5] { new VerticalWall(2, 0), new EmptySpace(2, 1), new EmptySpace(2, 2), new EmptySpace(1, 3), new VerticalWall(2, 4) }
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
    }
}