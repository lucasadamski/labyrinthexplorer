using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements.BuildingElements
{
    public class VerticalWall : Wall
    {
        public VerticalWall(int x, int y)
        {
            Name = "Vertical Wall";
            Model = '|';
            Position = new Coordinates(x, y);
        }
    }
}
