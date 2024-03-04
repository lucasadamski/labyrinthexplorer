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
        public bool Pickup(UserPlayer player);
        public InterActionDTO UseDoor(InterActionDTO input);
        public InterActionDTO DoDamage(InterActionDTO input);
        public InterActionDTO UseWeapon(InterActionDTO input);
    }
}
