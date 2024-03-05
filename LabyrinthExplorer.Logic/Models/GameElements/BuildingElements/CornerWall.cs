using LabyrinthExplorer.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements.BuildingElements
{
    public class CornerWall : Wall
    {
        public CornerWall(int x, int y)
        {
            Name = Settings.NAME_CORNER_WALL;
            Model = Settings.MODEL_CORNER_WALL;
            Position = new Coordinates(x, y);
        }
    }
}
