using LabyrinthExplorer.Logic.InternalCommunication;
using LabyrinthExplorer.Logic.Loggers;
using LabyrinthExplorer.Logic.Managers.MenuManager;
using LabyrinthExplorer.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs
{
    internal class InternalDTO
    {
        public InputAction InputAction { get; set; }
        internal Event Event { get; set; }
        internal DTO DTO { get; set; } = new DTO();
        internal Logger Logger { get; set; } = new Logger();
        internal Menu? Menu { get; set; }

        internal bool RequestUIInput = false;
    }
}
