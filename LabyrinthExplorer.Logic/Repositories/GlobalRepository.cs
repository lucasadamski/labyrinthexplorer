using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Data.Repositories
{
    public class GlobalRepository : IGlobalRepository
    {
        private Level injectedLevel = new Level();
        private Level testLevel;
        private Level level_01;
        private Level level_02;
        private Level level_03;
        private Level level_04;
        private Level level_05;

        private List<Level> levels = new List<Level>();

        public GlobalRepository()
        {
            testLevel = new Level()
            {
                Map = new char[5][]
                  {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                  },
                Name = Settings.TEST_LEVEL,
                Size = new Logic.Models.Coordinates(5, 5)
            };
            levels.Add(testLevel);

            level_01 = new Level()
            {
                Map = new char[5][]
                  {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', 'X', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', 'F' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                  },
                Name = Settings.LEVEL_01,
                Size = new Logic.Models.Coordinates(5, 5)
            };
            levels.Add(level_01);

            level_02 = new Level()
            {
                Map = new char[5][]
                  {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', '|', ' ', '|' }
                        , new char[5] { '|', ' ', '|', ' ', '|' }
                        , new char[5] { '|', ' ', 'D', ' ', 'F' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                  },
                Name = Settings.LEVEL_02,
                Size = new Logic.Models.Coordinates(5, 5)
            };
            levels.Add(level_02);

            level_03 = new Level()
            {
                Map = new char[10][]
                  {
                          new char[10] { '+', '-', '-', '-', '-', '-', '-', '-', '-', '+' }
                        , new char[10] { '|', 'P', ' ', ' ', '|', ' ', ' ', ' ', ' ', '|' }
                        , new char[10] { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', ' ', '|' }
                        , new char[10] { '|', ' ', 'X', ' ', 'D', ' ', ' ', 'X', 'K', '|' }
                        , new char[10] { '|', '-', '-', '-', '+', ' ', ' ', ' ', ' ', '|' }
                        , new char[10] { '|', ' ', ' ', ' ', '|', '-', '-', 'L', '-', '|' }
                        , new char[10] { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', ' ', '|' }
                        , new char[10] { '|', ' ', ' ', ' ', '|', ' ', ' ', 'X', ' ', '|' }
                        , new char[10] { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', ' ', 'F' }
                        , new char[10] { '+', '-', '-', '-', '+', '-', '-', '-', '-', '+' }
                  },
                Name = Settings.LEVEL_03,
                Size = new Logic.Models.Coordinates(5, 5)
            };
            levels.Add(level_03);

            level_04 = new Level()
            {
                Map = new char[10][]
                  {
                          new char[10] { '+', '-', '-', '-', '-', '-', '-', '-', '-', '+' }
                        , new char[10] { '|', 'P', ' ', '|', 'K', 'X', 'X', '|', ' ', '|' }
                        , new char[10] { '|', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', '|' }
                        , new char[10] { '|', 'X', ' ', '|', 'X', ' ', ' ', '|', 'X', '|' }
                        , new char[10] { '|', ' ', ' ', '|', ' ', ' ', ' ', 'L', ' ', '|' }
                        , new char[10] { '|', ' ', 'X', '|', 'L', '-', '-', '|', ' ', '|' }
                        , new char[10] { '|', ' ', 'X', '|', ' ', 'X', 'K', '|', ' ', '|' }
                        , new char[10] { '|', ' ', ' ', '|', ' ', 'X', ' ', '|', ' ', '|' }
                        , new char[10] { '|', 'K', ' ', 'L', ' ', ' ', ' ', '|', ' ', 'F' }
                        , new char[10] { '+', '-', '-', '-', '-', '-', '-', '-', '-', '+' }
                  },
                Name = Settings.LEVEL_04,
                Size = new Logic.Models.Coordinates(5, 5)
            };
            levels.Add(level_04);

            level_05 = new Level()
            {
                Map = new char[10][]
                  {
                          new char[10] { '+', '-', '-', '-', '-', '-', '-', '-', '-', '+' }
                        , new char[10] { '|', 'P', ' ', 'X', ' ', ' ', ' ', 'X', 'X', '|' }
                        , new char[10] { '|', ' ', ' ', ' ', ' ', 'X', ' ', ' ', 'X', '|' }
                        , new char[10] { '|', 'X', 'X', 'X', 'X', 'X', 'X', ' ', 'X', '|' }
                        , new char[10] { '|', 'X', 'X', 'X', 'X', 'X', 'X', ' ', 'X', '|' }
                        , new char[10] { '|', 'X', 'X', ' ', ' ', ' ', ' ', ' ', 'X', '|' }
                        , new char[10] { '|', ' ', ' ', ' ', 'X', 'X', 'X', 'X', 'X', '|' }
                        , new char[10] { '|', ' ', 'X', 'X', ' ', ' ', ' ', 'X', 'X', '|' }
                        , new char[10] { '|', ' ', ' ', ' ', ' ', 'X', ' ', ' ', ' ', 'F' }
                        , new char[10] { '+', '-', '-', '-', '-', '-', '-', '-', '-', '+' }
                  },
                Name = Settings.LEVEL_05,
                Size = new Logic.Models.Coordinates(5, 5)
            };
            levels.Add(level_05);



        }

        public GlobalRepository(char[][] injectedCanvas)
        {
            injectedLevel = new Level()
            {
                Map = injectedCanvas,
                Name = Settings.INJECTED_LEVEL,
                Size = new Logic.Models.Coordinates(injectedCanvas.Length, injectedCanvas[0].Length)
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>NEW INSTANCE of Level</returns>
        public Level GetLevel(string name)
        {
            Level level = new Level();
            if (name == Settings.TEST_LEVEL)
            {
                return testLevel;
            }
            else if (name == Settings.INJECTED_LEVEL)
            {
                return injectedLevel;
            }
           
            return testLevel;
        }

        public IEnumerable<Level> GetAllLevels() => new List<Level>() { level_01, level_02, level_03, level_04, level_05};
        
    }
}
