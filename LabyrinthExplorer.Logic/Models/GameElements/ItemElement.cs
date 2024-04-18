using LabyrinthExplorer.Data.DTOs;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    abstract public class ItemElement : GameElement
    {
        override public DTO Pickup(CharacterElement player)
        {
            DTO output = player.Pickup(this);
            if (output.Success == true)
            {
                output.AppendActionMessage($"{this.Name} has been picked up by {player.Name}");
                NotVisible = true;
                Position.X = 0;
                Position.Y = 0;
            }
            return output;
        }
    }
}
