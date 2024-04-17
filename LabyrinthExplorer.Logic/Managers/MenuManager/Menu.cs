using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.InternalCommunication;
using LabyrinthExplorer.Logic.Models;
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
        internal bool isActive;

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
            
            if (!isActive) //first initialization of menu, only showing options and waiting for input in 2nd cycle
            {
                TellMenuManagerINeedAnotherCycleAndInput(ref isActive, inputDTO);
                ActiveOptionIndex = 0;
                inputDTO.Logger.Log($"Menu: Title ${Title}. Active index {ActiveOptionIndex} Is Active {isActive} Received input {inputDTO.InputAction} ");
                return inputDTO;
            }
            else //2nd cycle
            {

                if (inputDTO.InputAction == Models.InputAction.Up)
                {
                    TellMenuManagerINeedAnotherCycleAndInput(ref isActive, inputDTO);
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
                    TellMenuManagerINeedAnotherCycleAndInput(ref isActive, inputDTO);
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
                    TellMenuManagerImQuitting(ref isActive, inputDTO);
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
                else if (inputDTO.InputAction == Models.InputAction.Use)
                {
                    TellMenuManagerImQuitting(ref isActive, inputDTO);
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
                else if (_MaxOptionIndex == 0 && inputDTO.InputAction == Models.InputAction.ExitToMenu) //if menu has only 1 option, you can exit it by ExitToMain menu action as well
                {
                    TellMenuManagerImQuitting(ref isActive, inputDTO);
                    
                    inputDTO.Event = Options.ElementAt(ActiveOptionIndex);
                    inputDTO.Logger.Log($"Menu: Event changed to ${inputDTO.Event}");
                    return inputDTO;
                }
                else
                {
                    inputDTO.RequestUIInput = true;
                    inputDTO.Logger.Log($"Menu: Title ${Title}. Active index {ActiveOptionIndex} Is Active {isActive} Received input {inputDTO.InputAction} ");
                    return inputDTO;
                }
            }
        }


        //Those methods are relevant for Menu Manager, it will command execution according to those values
        private void TellMenuManagerImQuitting(ref bool isActive, InternalDTO internalDTO)
        {
            isActive = false;
            internalDTO.RequestUIInput = false;
            internalDTO.InputAction = InputAction.Unknown;
        }
        private void TellMenuManagerINeedAnotherCycleAndInput(ref bool isActive, InternalDTO internalDTO)
        {
            isActive = true;
            internalDTO.RequestUIInput = true;
            internalDTO.InputAction = InputAction.Unknown;
        }
    }
}

