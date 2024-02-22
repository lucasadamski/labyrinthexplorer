using LabyrinthExplorer.Data.Repositories;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Logic.Models.GameElements;

namespace LabyrinthExplorer.DataTest
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void GetTestLevelTest()
        {
            IGlobalRepository repository = new GlobalRepository();

            Level level = repository.GetLevel("test");
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
                    Assert.AreEqual(map[i][j], level.Map[i][j]);
                }
            }
        }
    }
}