using LabyrinthExplorer.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class Weapon : ItemElement
    {
        public Weapon() 
        {
            Name = Settings.NAME_WEAPON;
            Model = Settings.MODEL_WEAPON;
        }
        public Weapon(int x, int y)
        {
            Name = Settings.NAME_WEAPON;
            Model = Settings.MODEL_WEAPON;
            Position = new Coordinates(x, y);
        }
        override public bool Pickup(CharacterElement player)
        {
            player.Inventory.Add(this);
            Visible = false;
            Position.X = 0;
            Position.Y = 0;
            return true;
        }
    }
}
