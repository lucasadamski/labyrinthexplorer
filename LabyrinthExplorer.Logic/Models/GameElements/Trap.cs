using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.DTOs;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class Trap : ItemElement
    {
        public Trap() 
        {
            Name = Settings.NAME_TRAP;
            Model = Settings.MODEL_TRAP;
        }
        public Trap(int x, int y)
        {
            Position = new Coordinates(x, y);
            Name = Settings.NAME_TRAP;
            Model = Settings.MODEL_TRAP;
        }
        override public DTO Pickup(CharacterElement player)
        {
            DTO output = player.DoDamage(Settings.TRAP_DAMAGE);
            if (output.Success == true)
            {
                output.AppendActionMessage($"{player.Name} has stepped on {this.Name}");
            }
            NotVisible = true;
            return output;
        }
        override public DTO ReceiveStep(CharacterElement player) => Pickup(player);

    }
}
