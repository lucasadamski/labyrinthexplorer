﻿using LabyrinthExplorer.Logic.Models;
using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.DTOs
{
    //Used as a internal communication protocol inside GameEngine between
    //GameEngine and HumanPlayer -> it sends a piece of GameMap to Player
    //along with input, so Player independly decides what to do with it
    public class InterActionDTO 
    {
        public GameElement[][] MapOfElements { get; set; } = new GameElement[3][];
        public DTO DTO { get; set; } = new DTO();
        public InputAction InputAction{ get; set; }
        public Coordinates CenterPosition { get; set; } = new Coordinates(1, 1);
    }
}
