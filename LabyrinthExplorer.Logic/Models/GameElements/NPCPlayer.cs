using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.DTOs;
using LabyrinthExplorer.Logic.DTOs;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class NPCPlayer : CharacterElement
    {
        private bool goingUp = true;

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

        override public InterActionDTO ReceiveInterActionDTO(InterActionDTO input)
        {
            InterActionDTO output = input;
            if (goingUp)
            {
                Coordinates tempPosition = new Coordinates(Position.X, Position.Y);
                output = MoveUp(input);
                if (tempPosition.X == Position.X && tempPosition.Y == Position.Y)
                {
                    goingUp = false;
                    output = MoveDown(input);
                }
            }
            else
            {
                Coordinates tempPosition = new Coordinates(Position.X, Position.Y);
                output = MoveDown(input);
                if (tempPosition.X == Position.X && tempPosition.Y == Position.Y)
                {
                    goingUp = true;
                    output = MoveUp(input);
                }
            }

                 
            return output;
        }

        override public DTO ReceiveStep(CharacterElement player) => DoDamage(player);

    }
}
