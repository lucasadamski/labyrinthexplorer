using LabyrinthExplorer.Logic.InternalCommunication;
using LabyrinthExplorer.Logic.Loggers;
using LabyrinthExplorer.Logic.Managers.MenuManager;
using LabyrinthExplorer.Logic.Models;
using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs
{
    internal class InternalDTO
    {
        public bool IsMenuActive { get; set; } = false;
        public bool IsApplicationActive { get; set; } = true;
        public InputAction InputAction { get; set; }
        internal Event Event { get; set; }
        internal DTO DTO { get; set; } = new DTO();
        internal Logger Logger { get; set; } = new Logger();
        internal Menu? Menu { get; set; }

        internal bool RequestUIInput = false;


        //Game Manager
        public UserPlayer? UserPlayer { get; set; } = new UserPlayer();
        public List<NPCPlayer> NPCPlayer { get; set; } = new List<NPCPlayer>();
        public List<ItemElement> Inventory { get; set; } = new List<ItemElement>();

        /*****************************
        * LevelManager
        * **************************/
        public Level Level { get; set; } = new Level();
        private List<Level> Levels { get; set; } = new List<Level>();
        public GameElement[][] Map { get; set; } = new GameElement[0][];
        internal char[][] Canvas { get; set; } = new char[0][];
        private int _currentLevelIndex = 0;
        /*****************************
         * UIManager
         * **************************/
        public GameEngineOutputDTO GameEngineOutputDTO { get; set; } = new GameEngineOutputDTO();
    }
}
