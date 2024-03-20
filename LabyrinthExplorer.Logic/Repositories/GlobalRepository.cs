using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
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
        private Level firstLevel;
        private Level secondLevel;

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

            firstLevel = new Level()
            {
                Map = new char[5][]
                  {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', 'F' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                  },
                Name = Settings.FIRST_LEVEL,
                Size = new Logic.Models.Coordinates(5, 5)
            };

            secondLevel = new Level()
            {
                Map = new char[5][]
                  {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', '|', ' ', '|' }
                        , new char[5] { '|', ' ', '|', ' ', '|' }
                        , new char[5] { '|', ' ', 'D', ' ', 'F' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                  },
                Name = Settings.SECOND_LEVEL,
                Size = new Logic.Models.Coordinates(5, 5)
            };

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

        public IEnumerable<Level> GetAllLevels() => new List<Level>() { firstLevel, secondLevel };
        
    }
}
