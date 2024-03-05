using LabyrinthExplorer.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements.BuildingElements
{
    public class HorizontalWall : Wall
    {
        public HorizontalWall(int x, int y)
        {
            Name = Settings.NAME_HORIZONTAL_WALL;
            Model = Settings.MODEL_HORIZONTAL_WALL;
            Position = new Coordinates(x, y);
        }
    }
}
