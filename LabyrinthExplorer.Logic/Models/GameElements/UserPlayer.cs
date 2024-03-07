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
            Name = Settings.NAME_USER_PLAYER;
            Model = Settings.MODEL_USER_PLAYER;
            Health = Settings.PLAYER_FULL_HEALTH;
            Position = new Coordinates(1, 1);
            HiddenElement = new EmptySpace(1, 1);
        }
        public UserPlayer(int x, int y)
        {
            Inventory = new List<ItemElement>();
            Name = Settings.NAME_USER_PLAYER;
            Model = Settings.MODEL_USER_PLAYER;
            Position = new Coordinates(x, y);
            Health = Settings.PLAYER_FULL_HEALTH;
            HiddenElement = new EmptySpace(x, y);
        }  

    }

}
