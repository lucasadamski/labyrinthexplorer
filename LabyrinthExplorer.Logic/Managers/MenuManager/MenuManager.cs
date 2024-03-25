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

        private Menu _mainNewGame = new Menu(MENU_TITLE_MAIN, Event.LevelNewGame, Event.UIQuitGame);
        private Menu _mainGamePause = new Menu(MENU_TITLE_MAIN, Event.GameStep, Event.LevelRestartCurrentLevel,Event.LevelNewGame, Event.UIQuitGame);
        private Menu _gameFinished = new Menu(MENU_TITLE_GAME_FINISHED, Event.MenuMainNewGame);
        private Menu _levelFinished = new Menu(MENU_TITLE_LEVEL_FINISHED, Event.LevelCheckNextLevel); 
        private Menu _gameOver = new Menu(MENU_TITLE_GAME_OVER, Event.MenuMainNewGame); 

        private Menu? _currentMenu = null;      

        public override InternalDTO ReceiveInternalDTO(InternalDTO inputDTO)
        {
            
                if (_currentMenu == null) //Switch to correct Menu based on Event type
                {
                    if (inputDTO.Event == InternalCommunication.Event.MenuMainNewGame)
                    {
                        _currentMenu = _mainNewGame;                   
                        return _currentMenu.ReceiveDTO(inputDTO);
                    }
                    if (inputDTO.Event == InternalCommunication.Event.MenuMainPaused)
                    {
                        _currentMenu = _mainGamePause;                        
                        return _currentMenu.ReceiveDTO(inputDTO);
                    }
                    else if (inputDTO.Event == InternalCommunication.Event.MenuGameOver)
                    {
                        _currentMenu = _gameOver;                       
                        return _currentMenu.ReceiveDTO(inputDTO);
                    }
                    else if (inputDTO.Event == InternalCommunication.Event.MenuLevelFinished)
                    {
                        _currentMenu = _levelFinished;                      
                        return _currentMenu.ReceiveDTO(inputDTO);
                    }
                    else if (inputDTO.Event == InternalCommunication.Event.MenuGameSummary)
                    {
                        _currentMenu = _gameFinished;                        
                        return _currentMenu.ReceiveDTO(inputDTO);
                    }
                    else
                    {
                        return inputDTO;
                    }
                }
                else
                {
                    return _currentMenu.ReceiveDTO(inputDTO);
                }
        }
    }
}
