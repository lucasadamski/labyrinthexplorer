using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class EmptySpace : GameElement
    {
        public EmptySpace() { }
        public EmptySpace(int x, int y)
        {
            Name = "Empty Space";
            Position = new Coordinates(x, y);
        }
    }
}
