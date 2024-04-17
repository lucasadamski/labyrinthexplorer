using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Data.Helpers
{
    public static class Settings
    {
        //logger type message
        public const string LOGGER_DEBUG = "<debug>";
        public const string LOGGER_ACTION = "<action>";
        public const string LOGGER_ERROR = "*<ERROR>*";

        public const string PLAYER_NAME = "User Player";

        //UI will use those to translate to sprites
        public const string NAME_USER_PLAYER    = "User Player";
        public const string NAME_NPC_PLAYER     = "NPC Player";
        public const string NAME_KEY            = "Key";
        public const string NAME_WEAPON         = "Weapon";
        public const string NAME_TRAP           = "Trap";
        public const string NAME_OPEN_DOOR      = "Open Door";
        public const string NAME_CLOSED_DOOR    = "Closed Door";
        public const string NAME_DOOR           = "Door";
        public const string NAME_CORNER_WALL    = "Corner Wall";
        public const string NAME_HORIZONTAL_WALL ="Horizontal Wall";
        public const string NAME_VERTICAL_WALL  = "Vertical Wall";
        public const string NAME_EMPTY_SPACE    = "Empty Space";
        public const string NAME_FINISH_LEVEL_PORTAL = "Portal To Next Level";

        public const int PLAYER_FULL_HEALTH = 100;
        public const int NPC_PLAYER_FULL_HEALTH = 100;

        public const int TRAP_DAMAGE = 25;
        public const int ENEMY_DAMAGE = 50;
        public const int WEAPON_DAMAGE = 50;

        //UI will use those to translate to sprites
        public const char MODEL_USER_PLAYER     = 'P';
        public const char MODEL_NPC_PLAYER      = 'E';
        public const char MODEL_KEY             = 'K';
        public const char MODEL_WEAPON          = 'W';
        public const char MODEL_TRAP            = 'X';
        public const char MODEL_OPEN_DOOR       = 'O';
        public const char MODEL_CLOSED_DOOR     = 'D';
        public const char MODEL_CORNER_WALL     = '+';
        public const char MODEL_HORIZONTAL_WALL = '-';
        public const char MODEL_VERTICAL_WALL   = '|';
        public const char MODEL_EMPTY_SPACE     = ' ';
        public const char MODEL_FINISH_LEVEL_PORTAL  = 'F';

        //Key messages that mechanics depend on
        public const string MESSAGE_LEVEL_FINISHED = "Level finished";
        public const string MESSAGE_GAME_OVER = "Game over!";
        public const string MESSAGE_GAME_FINISHED = "Game finished!";

        //Levels names
        public const string ALL_LEVELS = "all_levels";
        public const string TEST_LEVEL = "test_level";
        public const string INJECTED_LEVEL = "injected_level";
        public const string LEVEL_01 = "level_1";
        public const string LEVEL_02 = "level_2";
        public const string LEVEL_03 = "level_3";
        public const string LEVEL_04 = "level_4";
        public const string LEVEL_05 = "level_5";
        public const string LEVEL_06 = "level_6";
        public const string LEVEL_07 = "level_7";
        public const string LEVEL_08 = "level_8";

        //Menu messages
        public const string MENU_TITLE_MAIN = "Main Menu";
        public const string MENU_TITLE_LEVEL_FINISHED = "Level finished";
        public const string MENU_TITLE_GAME_OVER = "Game over!";
        public const string MENU_TITLE_GAME_FINISHED = "Game finished!";
        public const string MENU_OPTION_NEW_GAME = "New Game";
        public const string MENU_OPTION_RESUME_GAME = "Resume Game";
        public const string MENU_OPTION_EXIT = "Exit Game";
        public const string MENU_OPTION_RESTART_LEVEL = "Restart Level";
        public const string MENU_OPTION_CONTINUE = "Continue";

        //Console display
        public const bool CONSOLE_UI_DISPLAY_DEBUG_LOG = true;        
        public const bool CONSOLE_UI_DISPLAY_CLEAR_SCREEN_PER_FRAME = true;


        //HUD UserPlayer direct word
        public const string USER_PLAYER_YOU = "You";
        public const string USER_PLAYER_YOU_HAVE = "You've";
        public const string USER_PLAYER_YOU_ARE = "You are";
    }                       
}
