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
        public DTO Pickup(ItemElement item);
        public DTO Use(CharacterElement player);
        public DTO DoDamage(CharacterElement playerDoneDamageTo);
        public DTO DoDamage(byte amountOfDamage);
        public DTO ReceiveStep(CharacterElement player);
    }
}
