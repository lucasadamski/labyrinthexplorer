using LabyrinthExplorer.Data.Helpers;
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
        public Key() 
        {
            Name = Settings.NAME_KEY;
            Model = Settings.MODEL_KEY;
        }
        public Key(int x, int y) 
        {
            Name = Settings.NAME_KEY;
            Model = Settings.MODEL_KEY;
            Position = new Coordinates(x, y);
        }
        override public bool Pickup(CharacterElement player)
        {
            player.Inventory.Add(this);
            NotVisible = true;
            Position.X = 0;
            Position.Y = 0;
            return true;
        }
    }
}
