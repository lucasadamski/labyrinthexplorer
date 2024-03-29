﻿using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Infrastructure
{
    public interface IMove
    {
        public InterActionDTO ReceiveInterActionDTO(InterActionDTO input);
        public InterActionDTO MoveUp(InterActionDTO input);
        public InterActionDTO MoveDown(InterActionDTO input);
        public InterActionDTO MoveLeft(InterActionDTO input);
        public InterActionDTO MoveRight(InterActionDTO input);
        public InterActionDTO UseWeapon(InterActionDTO input); //uses only weapon
        public InterActionDTO UseAction(InterActionDTO input); //uses usable items in vincinity - eg door 

    }
}
