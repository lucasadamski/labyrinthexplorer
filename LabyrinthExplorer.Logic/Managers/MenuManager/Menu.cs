using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.InternalCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Managers.MenuManager
{
    public class Menu
    {
        public string Title { get; set; }
        public List<Event> Options { get; set; } = new List<Event>();
        public int ActiveOptionIndex { get; set; } = 0;
        public bool IsActive { get; set; } = false;

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
            inputDTO.Logger.Log($"Menu: Title ${Title}. Active index {ActiveOptionIndex} Is Active {IsActive} Received input {inputDTO.InputAction} ");
            if (!IsActive)
            {
                IsActive = true;
                return inputDTO;
            }
            else
            {
                if (inputDTO.InputAction == Models.InputAction.Up)
                {
                    if (_MaxOptionIndex == 0) return inputDTO;
                    if (ActiveOptionIndex == 0) 
                        ActiveOptionIndex = _MaxOptionIndex;
                    else 
                        ActiveOptionIndex--;
                    return inputDTO;
                }
                else if (inputDTO.InputAction == Models.InputAction.Down)
                {
                    if (_MaxOptionIndex == 0) return inputDTO;
                    if (ActiveOptionIndex == _MaxOptionIndex) 
                        ActiveOptionIndex = 0;
                    else 
                        ActiveOptionIndex++;
                    return inputDTO;
                }
                else if (inputDTO.InputAction == Models.InputAction.Use)
                {
                    IsActive = false;
                    if (_MaxOptionIndex == 0)
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
                    inputDTO.RequestUIInput = true;
                    return inputDTO;
                }
            }
            return inputDTO;

        }
    }
}
