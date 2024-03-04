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
        virtual public bool Pickup(UserPlayer player)
        {
            return false;
        }
        virtual public InterActionDTO DoDamage(InterActionDTO input)
        {
            return input;
        }
        virtual public InterActionDTO UseDoor(InterActionDTO input)
        {
            return input;
        }
        virtual public InterActionDTO UseWeapon(InterActionDTO input)
        {
            return input;
        }
    }
}
