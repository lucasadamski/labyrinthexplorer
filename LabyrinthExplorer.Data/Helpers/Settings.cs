using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Data.Helpers
{
    public static class Settings
    {
        public const string TEST_LEVEL = "test_level";
        public const string INJECTED_LEVEL = "injected_level";

        public const string PLAYER_NAME = "User Player";

        public const int PLAYER_FULL_HEALTH = 100;
        public const int TRAP_DAMAGE = 25;
        public const int ENEMY_DAMAGE = 50;

        //UI will use those to translate to sprites
        public const char MODEL_USER_PLAYER     = 'P';
        public const char MODEL_NPC_PLAYER      = 'E';
        public const char MODEL_KEY             = 'K';
        public const char MODEL_WEAPON          = 'W';
        public const char MODEL_TRAP            = 'X';
        public const char MODEL_OPEN_DOOR       = 'O';
        public const char MODEL_CORNER_WALL     = '+';
        public const char MODEL_HORIZONTAL_WALL = '-';
        public const char MODEL_VERTICAL_WALL   = '|';
    }
}
