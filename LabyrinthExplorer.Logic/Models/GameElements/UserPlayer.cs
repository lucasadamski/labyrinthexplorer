using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class UserPlayer : CharacterElement
    {
        public UserPlayer() { }
        public UserPlayer(int x, int y)
        {
            Name = "User Player";
            Position = new Coordinates(x, y);
        }
    }
}
