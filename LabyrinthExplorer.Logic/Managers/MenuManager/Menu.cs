using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.InternalCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("LabyrinthExplorer.Test")]
namespace LabyrinthExplorer.Logic.Managers.MenuManager
{
    public class Menu
    {
        internal string Title { get; set; }
        internal string Message { get; set; } = "";
        internal List<Event> Options { get; set; } = new List<Event>();
        internal int ActiveOptionIndex { get; set; } = 0;
        internal bool isActive { get; set; } = false;

        private int _MaxOptionIndex = 0;


        public Menu() { }
        public Menu(string title, params Event[] options)
        {
            Title = title;
            try
            {
                Options.AddRange(options);
            }
            catch (Exception) { }

            if (Options.Count > 0)
                _MaxOptionIndex = Options.Count() - 1;
            else
                _MaxOptionIndex = 0;
        }

        internal InternalDTO ReceiveDTO(InternalDTO inputDTO)
        {
            
            if (!isActive)
            {
                isActive = true;
                inputDTO.RequestUIInput = true;
                ActiveOptionIndex = 0;
                inputDTO.Logger.Log($"Menu: Title ${Title}. Active index {ActiveOptionIndex} Is Active {isActive} Received input {inputDTO.InputAction} ");
                return inputDTO;
            }
            else
            {
                //todo false
                if (inputDTO.InputAction == Models.InputAction.Up)
                {
                    inputDTO.RequestUIInput = true;
                    if (_MaxOptionIndex == 0) return inputDTO;
                    if (ActiveOptionIndex == 0) 
                        ActiveOptionIndex = _MaxOptionIndex;
                    else 
                        ActiveOptionIndex--;
                    inputDTO.Logger.Log($"Menu: Title ${Title}. Active index {ActiveOptionIndex} Is Active {isActive} Received input {inputDTO.InputAction} ");
                    return inputDTO;
                }
                else if (inputDTO.InputAction == Models.InputAction.Down)
                {
                    inputDTO.RequestUIInput = true;
                    if (_MaxOptionIndex == 0) return inputDTO;
                    if (ActiveOptionIndex == _MaxOptionIndex) 
                        ActiveOptionIndex = 0;
                    else 
                        ActiveOptionIndex++;
                    inputDTO.Logger.Log($"Menu: Title ${Title}. Active index {ActiveOptionIndex} Is Active {isActive} Received input {inputDTO.InputAction} ");
                    return inputDTO;
                }
                else if (inputDTO.InputAction == Models.InputAction.Use)
                {
                    isActive = false;
                    inputDTO.RequestUIInput = false;
                    if (Options.Count() == 0)
                    {
                        inputDTO.Event = Event.MenuMainNewGame;
                        inputDTO.Logger.Log($"ERROR: Menu: Event changed to ${inputDTO.Event} because there are no options list in Menu");
                        return inputDTO;
                    }
                    else
                    {
                        inputDTO.Event = Options.ElementAt(ActiveOptionIndex);
                        inputDTO.Logger.Log($"Menu: Event changed to ${inputDTO.Event}");
                        return inputDTO;
                    }                    
                }
                else
                {
                    inputDTO.Logger.Log($"Menu: Title ${Title}. Active index {ActiveOptionIndex} Is Active {isActive} Received input {inputDTO.InputAction} ");
                    return inputDTO;
                }
            }
        }
    }
}
