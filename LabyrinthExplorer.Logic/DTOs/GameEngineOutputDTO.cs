using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.DTOs
{
    public class GameEngineOutputDTO //Communication Protocol between UI and GameEngine
    {
        public char[][] Frame { get; set; }
        public DTO DTO { get; set; }
        public string Log { get; set; }
        public string HUD { get; set; }

    }
}
