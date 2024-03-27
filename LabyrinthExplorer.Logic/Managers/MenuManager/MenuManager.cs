using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.InternalCommunication;
using LabyrinthExplorer.Logic.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabyrinthExplorer.Data.Helpers.Settings;

namespace LabyrinthExplorer.Logic.Managers.MenuManager
{
    internal class MenuManager : Manager
    {
        public InternalDTO ManagerDTO { get; set; }
        public MenuManager(InternalDTO internalDTO)
        {
            ManagerDTO = internalDTO;
            ManagerDTO.Menu = _currentMenu;
        }

        private Menu _mainNewGame = new Menu(MENU_TITLE_MAIN, Event.LevelNewGame, Event.UIQuitGame); //TODO Test gamestep
        private Menu _mainGamePause = new Menu(MENU_TITLE_MAIN, Event.GameStep, Event.LevelRestartCurrentLevel,Event.LevelNewGame, Event.UIQuitGame);
        private Menu _gameFinished = new Menu(MENU_TITLE_GAME_FINISHED, Event.MenuMainNewGame);
        private Menu _levelFinished = new Menu(MENU_TITLE_LEVEL_FINISHED, Event.LevelCheckNextLevel); 
        private Menu _gameOver = new Menu(MENU_TITLE_GAME_OVER, Event.MenuMainNewGame); 

        private Menu? _currentMenu = null;      

        public override InternalDTO ReceiveInternalDTO(InternalDTO inputDTO)
        {

            inputDTO.RequestUIInput = false;
            if (_currentMenu == null) //Switch to correct Menu based on Event type, this is a problem
            {
                if (inputDTO.Event == InternalCommunication.Event.MenuMainNewGame)
                {
                    _currentMenu = _mainNewGame;
                    inputDTO.Menu = _currentMenu;
                    inputDTO = _currentMenu.ReceiveDTO(inputDTO); //???? nie chce wyjść z menu
                    inputDTO.IsMenuActive = _currentMenu.isActive;
                    if (!inputDTO.IsApplicationActive) inputDTO.Logger.Log($"MenuManager: Menu exited with event {inputDTO.Event}");
                    return inputDTO;
                }
                if (inputDTO.Event == InternalCommunication.Event.MenuMainPaused)
                {
                    _currentMenu = _mainGamePause;
                    inputDTO.Menu = _currentMenu;
                    inputDTO = _currentMenu.ReceiveDTO(inputDTO);
                    inputDTO.IsMenuActive = _currentMenu.isActive;
                    if (!inputDTO.IsApplicationActive) inputDTO.Logger.Log($"MenuManager: Menu exited with event {inputDTO.Event}");
                    return inputDTO;
                }
                else if (inputDTO.Event == InternalCommunication.Event.MenuGameOver)
                {
                    _currentMenu = _gameOver;
                    inputDTO.Menu = _currentMenu;
                    inputDTO = _currentMenu.ReceiveDTO(inputDTO);
                    inputDTO.IsMenuActive = _currentMenu.isActive;
                    if (!inputDTO.IsApplicationActive) inputDTO.Logger.Log($"MenuManager: Menu exited with event {inputDTO.Event}");
                    return inputDTO;
                }
                else if (inputDTO.Event == InternalCommunication.Event.MenuLevelSummary)
                {
                    _currentMenu = _levelFinished;
                    inputDTO.Menu = _currentMenu;
                    inputDTO = _currentMenu.ReceiveDTO(inputDTO);
                    inputDTO.IsMenuActive = _currentMenu.isActive;
                    if (!inputDTO.IsApplicationActive) inputDTO.Logger.Log($"MenuManager: Menu exited with event {inputDTO.Event}");
                    return inputDTO;
                }
                else if (inputDTO.Event == InternalCommunication.Event.MenuGameSummary)
                {
                    _currentMenu = _gameFinished;
                    inputDTO.Menu = _currentMenu;
                    inputDTO = _currentMenu.ReceiveDTO(inputDTO);
                    inputDTO.IsMenuActive = _currentMenu.isActive;
                    if (!inputDTO.IsApplicationActive) inputDTO.Logger.Log($"MenuManager: Menu exited with event {inputDTO.Event}");
                    return inputDTO;
                }
                else
                {
                    return inputDTO;
                }
            }
            else
            {
                inputDTO = _currentMenu.ReceiveDTO(inputDTO);
                inputDTO.IsMenuActive = _currentMenu.isActive; //does menu exited?
                if (!inputDTO.IsMenuActive) _currentMenu = null; //menu exits
                if (!inputDTO.IsApplicationActive) inputDTO.Logger.Log($"MenuManager: Menu exited with event {inputDTO.Event}");
                return inputDTO;
            }
        }
    }
}
