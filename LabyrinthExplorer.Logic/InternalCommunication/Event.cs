using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.InternalCommunication
{
    public enum Event
    {
        GameStep,
        MenuGameSummary,
        MenuMainPaused,
        MenuMainNewGame,
        MenuLevelSummary,
        MenuGameOver,
        LevelCheckNextLevel,
        LevelLoadNext,
        LevelNewGame,
        LevelRestartCurrentLevel,
        UIQuitGame
    }
}
