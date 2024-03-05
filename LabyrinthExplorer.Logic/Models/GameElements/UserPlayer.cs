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
    public class UserPlayer : CharacterElement, IMove
    {
        public UserPlayer() 
        {
            Inventory = new List<ItemElement>();
        }
        public UserPlayer(int x, int y)
        {
            Inventory = new List<ItemElement>();
            Name = Settings.PLAYER_NAME;
            Position = new Coordinates(x, y);
            Model = 'P';
            Health = Settings.PLAYER_FULL_HEALTH;
        }  

    }

}
