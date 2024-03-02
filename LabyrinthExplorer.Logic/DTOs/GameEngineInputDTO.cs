using LabyrinthExplorer.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.DTOs
{
    public class GameEngineInputDTO
    {
        public InputAction InputAction { get; set; } = InputAction.Unknown;
        public DTO DTO { get; set; } = new DTO();

    }
}
