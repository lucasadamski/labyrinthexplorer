using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using LabyrinthExplorer.Logic.Managers.MenuManager;
using LabyrinthExplorer.Logic.InternalCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyrinthExplorer.Logic.Models;

namespace LabyrinthExplorer.Test
{
    [TestClass]
    public class MenuManagerTests
    {
        [TestMethod]
        public void MenuErrorInitializationTest()
        {

            InternalDTO internalDTO = new InternalDTO()
            {
                InputAction = Logic.Models.InputAction.Down,
                Event = Event.MenuMainNewGame,
                DTO = new Logic.DTOs.DTO(),
                Logger = new Logic.Loggers.Logger()
            };
            Menu menu = new Menu("Test Menu");

            menu.ReceiveDTO(internalDTO);
            Assert.AreEqual(0, menu.ActiveOptionIndex);

            menu.ReceiveDTO(internalDTO);
            Assert.AreEqual(0, menu.ActiveOptionIndex);
        }


        [TestMethod]
        public void MenuTest()
        {
        
            InternalDTO internalDTO = new InternalDTO()
            {
                InputAction = Logic.Models.InputAction.Down,
                Event = Event.MenuMainNewGame,
                DTO = new Logic.DTOs.DTO(),
                Logger = new Logic.Loggers.Logger()
            };
            Menu menu = new Menu("Test Menu", Event.LevelNewGame, Event.GameStep, Event.LevelRestartCurrentLevel, Event.UIQuitGame);

            menu.ReceiveDTO(internalDTO);
            Assert.AreEqual(0, menu.ActiveOptionIndex);

            menu.ReceiveDTO(internalDTO);
            Assert.AreEqual(1, menu.ActiveOptionIndex);

            menu.ReceiveDTO(internalDTO);
            Assert.AreEqual(2, menu.ActiveOptionIndex);
            Assert.AreEqual(Event.MenuMainNewGame, internalDTO.Event);

            internalDTO.InputAction = InputAction.Use;
            menu.ReceiveDTO(internalDTO);
            Assert.AreEqual(2, menu.ActiveOptionIndex);
            Assert.AreEqual(Event.LevelRestartCurrentLevel, internalDTO.Event);

            //1 item test
            internalDTO = new InternalDTO()
            {
                InputAction = Logic.Models.InputAction.Use,
                Event = Event.MenuMainNewGame,
                DTO = new Logic.DTOs.DTO(),
                Logger = new Logic.Loggers.Logger()
            };
            menu = new Menu("Test Menu", Event.LevelNewGame);

            var output = menu.ReceiveDTO(internalDTO);
            output = menu.ReceiveDTO(internalDTO);
            Assert.AreEqual(Event.LevelNewGame, output.Event);

        }
    }
}
