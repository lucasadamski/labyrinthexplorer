using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class Trap : ItemElement
    {
        public Trap() { }
        public Trap(int x, int y)
        {
            Position = new Coordinates(x, y);
            Name = "Trap";
        }
        override public bool Pickup(CharacterElement player)
        {
            player.Health -= Settings.TRAP_DAMAGE;//reimplement with interface
            Visible = false;
            return true;
        }

    }
}
