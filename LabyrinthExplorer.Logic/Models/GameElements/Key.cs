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
        override public DTO Pickup(CharacterElement player)
        {
            DTO output = new DTO($"{this.Name} has been picked up by {player.Name}");
            player.Inventory.Add(this); //TODO change to public method
            NotVisible = true;
            Position.X = 0;
            Position.Y = 0;
            return output;
        }
    }
}
