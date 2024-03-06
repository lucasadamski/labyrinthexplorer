using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;

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
            DTO output = new DTO($"{this.Name} has been stepped on by {player.Name}");
            player.Health -= Settings.TRAP_DAMAGE;//reimplement with interface
            NotVisible = true;
            return output;
        }

    }
}
