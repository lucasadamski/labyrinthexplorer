using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    abstract public class ItemElement : GameElement, IInteract
    {
        //virtual public DTO DoDamage(CharacterElement playerDoneDamageTo) => new DTO("Not implemented\n", false);
        //virtual public DTO DoDamage(byte amountOfDamage) => new DTO("Not implemented\n", false);
        //virtual public DTO Pickup(CharacterElement player) => new DTO("Not implemented\n", false);
        //virtual public DTO Use(CharacterElement player) => new DTO("Not implemented\n", false);
    }
}
