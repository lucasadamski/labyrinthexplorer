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
            Name = "Corner Wall";
            Model = '+';
            Position = new Coordinates(x, y);
        }
    }
}
