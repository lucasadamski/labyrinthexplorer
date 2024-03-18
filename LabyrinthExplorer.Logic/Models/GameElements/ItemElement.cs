using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
