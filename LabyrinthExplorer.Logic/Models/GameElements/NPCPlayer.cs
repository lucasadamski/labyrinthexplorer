using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            Health = Settings.NPC_PLAYER_FULL_HEALTH;
        }
        public NPCPlayer(int x, int y)
        {
            Name = Settings.NAME_NPC_PLAYER;
            Model = Settings.MODEL_NPC_PLAYER;
            Health = Settings.NPC_PLAYER_FULL_HEALTH;
            Position = new Coordinates(x, y);
        }
        public List<Coordinates> PatrolMap { get; set; } = new List<Coordinates>();

        override public DTO DoDamage(CharacterElement playerDoneDamageTo)
        {
            DTO output = new DTO();
            if (playerDoneDamageTo is UserPlayer up)
            {
                output.Message += $"{this.Name} has done {Settings.ENEMY_DAMAGE} damage to {playerDoneDamageTo.Name}\n";
                DTO temp = up.DoDamage(Settings.ENEMY_DAMAGE);
                output.Message += temp.Message;
                return output;
            }
            return new DTO(false);
        }
    }
}
