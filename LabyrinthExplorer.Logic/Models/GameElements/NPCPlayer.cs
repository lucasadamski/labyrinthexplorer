using LabyrinthExplorer.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class NPCPlayer : CharacterElement
    {
        public NPCPlayer() 
        {
            Name = Settings.NAME_NPC_PLAYER;
            Model = Settings.MODEL_NPC_PLAYER;
        }
        public NPCPlayer(int x, int y)
        {
            Name = Settings.NAME_NPC_PLAYER;
            Model = Settings.MODEL_NPC_PLAYER;
            Position = new Coordinates(x, y);
        }
        public List<Coordinates> PatrolMap { get; set; } = new List<Coordinates>();
    }
}
