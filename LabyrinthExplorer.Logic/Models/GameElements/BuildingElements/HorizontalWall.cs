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
            Name = "Horizontal Wall";
            Model = '-';
            Position = new Coordinates(x, y);
        }
    }
}
