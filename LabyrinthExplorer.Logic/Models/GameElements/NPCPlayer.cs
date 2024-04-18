using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.DTOs;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class NPCPlayer : CharacterElement
    {
        public NPCPlayer() 
        {
            Name = Settings.NAME_NPC_PLAYER;
            Model = Settings.MODEL_NPC_PLAYER;
            AlternateModel = Settings.MODEL_EMPTY_SPACE;
            Health = Settings.NPC_PLAYER_FULL_HEALTH;
            HiddenElement = new EmptySpace();
        }
        public NPCPlayer(int x, int y)
        {
            Name = Settings.NAME_NPC_PLAYER;
            Model = Settings.MODEL_NPC_PLAYER;
            AlternateModel = Settings.MODEL_EMPTY_SPACE;
            Health = Settings.NPC_PLAYER_FULL_HEALTH;
            Position = new Coordinates(x, y);
            HiddenElement = new EmptySpace(x, y);
        }
        public List<Coordinates> PatrolMap { get; set; } = new List<Coordinates>();

        override public DTO DoDamage(CharacterElement playerDoneDamageTo) //delete if below works
        {
            DTO output = new DTO();
            if (playerDoneDamageTo is UserPlayer up)
            {
                output.AppendActionMessage($"{this.Name} has done {Settings.ENEMY_DAMAGE} damage to {playerDoneDamageTo.Name}");
                DTO temp = up.DoDamage(Settings.ENEMY_DAMAGE);
                output.AppendEditedMessage(temp.Message);
                return output;
            }
            return new DTO(false);
        }

        override public DTO ReceiveStep(CharacterElement player) => DoDamage(player);

    }
}
