using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Infrastructure
{
    public interface IInteract
    {
        public bool Pickup(CharacterElement player);
        public bool UseDoor(CharacterElement player);
        public bool DoDamage(CharacterElement playerDoneDamageTo);
    }
}
