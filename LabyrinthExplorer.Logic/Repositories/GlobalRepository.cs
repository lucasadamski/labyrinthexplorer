using LabyrinthExplorer.Data.Repositories.Infrastructure;
using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Data.Repositories
{
    public class GlobalRepository : IGlobalRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>NEW INSTANCE of Level</returns>
        public Level GetLevel(string name)
        {
            Level level = new Level();
            if (name == "test")
            {
                level.Map = new char[5][]
                  {
                          new char[5] { '+', '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                  };
                level.Name = "test";
                level.Size = new Logic.Models.Coordinates(level.Map.Length, level.Map[0].Length);
            }
            else
            {
                level.Map = new char[5][]
                  {
                          new char[5] { '+' , '-', '-', '-', '+'}
                        , new char[5] { '|', 'P', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '|', ' ', ' ', ' ', '|' }
                        , new char[5] { '+', '-', '-', '-', '+' }
                  };
                level.Name = "test";
                level.Size = new Logic.Models.Coordinates(level.Map.Length, level.Map[0].Length);
            }
            return level;
        }
    }
}
