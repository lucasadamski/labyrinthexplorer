using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class Weapon : ItemElement
    {
        public Weapon() { }
        public Weapon(int x, int y)
        {
            Name = "Weapon";
            Position = new Coordinates(x, y);
        }
    }
}
