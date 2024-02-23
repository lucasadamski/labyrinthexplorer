using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class NPCPlayer : CharacterElement
    {
        public NPCPlayer() { }
        public NPCPlayer(int x, int y)
        {
            Name = "NPC Player";
            Position = new Coordinates(x, y);
        }
        public List<Coordinates> PatrolMap { get; set; } = new List<Coordinates>();
    }
}
