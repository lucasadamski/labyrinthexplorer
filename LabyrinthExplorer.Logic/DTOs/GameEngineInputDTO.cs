using LabyrinthExplorer.Data.DTOs;
using LabyrinthExplorer.Logic.InternalCommunication;
using LabyrinthExplorer.Logic.Models;

namespace LabyrinthExplorer.Logic.DTOs
{
    public class GameEngineInputDTO //Communication Protocol between UI and GameEngine
    {
        public InputAction InputAction { get; set; } = InputAction.Unknown;
        public DTO DTO { get; set; } = new DTO();
        public Event? OverridenEvent { get; set; }

    }
}
