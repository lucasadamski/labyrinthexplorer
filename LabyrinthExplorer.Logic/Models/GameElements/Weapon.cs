using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
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
     
        override public DTO ReceiveStep(CharacterElement player) => Pickup(player);
    }
}
