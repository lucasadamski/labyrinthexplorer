using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
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
        override public bool Pickup(UserPlayer player)
        {
            player.Inventory.Add(this);
            Visible = false;
            Position.X = 0;
            Position.Y = 0;
            return true;
        }

        override public InterActionDTO DoDamage(InterActionDTO input)
        {
            return input;
        }
        override public InterActionDTO UseDoor(InterActionDTO input)
        {
            return input;
        }
        override public InterActionDTO UseWeapon(InterActionDTO input)
        {
            return input;
        }
    }
}
