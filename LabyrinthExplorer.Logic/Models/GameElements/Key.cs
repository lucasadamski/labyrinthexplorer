using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class Key : ItemElement
    {
        public Key() { }
        public Key(int x, int y) 
        {
            Name = "Key";
            Position = new Coordinates(x, y);
        }
    }
}
