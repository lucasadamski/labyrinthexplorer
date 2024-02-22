using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class Trap : BuildingElement
    {
        public Trap() { }
        public Trap(int x, int y)
        {
            Name = "Trap";
            Position.X = x;
            Position.Y = y;
        }
    }
}
