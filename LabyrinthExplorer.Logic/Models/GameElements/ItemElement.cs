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
        virtual public bool DoDamage(CharacterElement playerDoneDamageTo) => false;
        virtual public bool Pickup(UserPlayer player) => false;
        virtual public bool UseDoor(UserPlayer player) => false;
    }
}
