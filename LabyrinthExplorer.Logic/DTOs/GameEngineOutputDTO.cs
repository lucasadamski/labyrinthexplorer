﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.DTOs
{
    public class GameEngineOutputDTO //Communication Protocol between UI and GameEngine
    {
        public char[][] Frame { get; set; }
        public DTO DTO { get; set; } = new DTO();
        public string Log { get; set; } = "";
        public string HUD { get; set; } = ""; //not implemented
        public string LevelSummary { get; set; } = "";
        public string? Menu { get; set; }

        public bool IsGameActive { get; set; } = false;
        public bool IsApplicationActive { get; set; } = true;

    }
}
