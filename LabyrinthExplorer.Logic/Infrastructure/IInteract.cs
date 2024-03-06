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
        public DTO Pickup(CharacterElement player);
        public DTO UseDoor(CharacterElement player);
        public DTO DoDamage(CharacterElement playerDoneDamageTo);
        public DTO DoDamage(byte amountOfDamage);
    }
}
