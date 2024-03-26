using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.Loggers;
using LabyrinthExplorer.Logic.Models.GameElements;
using LabyrinthExplorer.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyrinthExplorer.Logic.InternalCommunication;

namespace LabyrinthExplorer.Logic.Managers.MenuManager
{
    internal class UIManager : Manager
    {
        public override InternalDTO ReceiveInternalDTO(InternalDTO inputDTO)
        {
            
            if (inputDTO.Event == Event.UIQuitGame)
            {
                inputDTO.Logger.Log($"UIManager: Event {inputDTO.Event} - requested to quit application");
                inputDTO.IsApplicationActive = false;
                inputDTO.RequestUIInput = true;
                return inputDTO;
            }
            else
            {
                inputDTO.Logger.Log($"UIManager: Event {inputDTO.Event} can't be processed - requested event {Event.MenuMainNewGame}");
                inputDTO.Event = Event.MenuMainNewGame;
                return inputDTO;
            }
        }
    }
}
