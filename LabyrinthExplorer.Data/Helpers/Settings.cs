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
        public const bool CONSOLE_UI_DISPLAY_DEBUG_LOG = false;        
        public const bool CONSOLE_UI_DISPLAY_CLEAR_SCREEN_PER_FRAME = true;


        //HUD UserPlayer direct word
        public const string USER_PLAYER_YOU = "You";
        public const string USER_PLAYER_YOU_HAVE = "You've";
        public const string USER_PLAYER_YOU_ARE = "You are";

        //Colors of ConsoleUI
        /*
         * Black	0	
        Blue	9
        Cyan	11
        DarkBlue	1
        DarkCyan	3
        DarkGray	8
        DarkGreen	2
        DarkMagenta	5
        DarkRed	4
        DarkYellow	6
        Gray	7
        Green	10
        Magenta	13
        Red	12
        White	15
        Yellow	14	
         */
        public const int COLOR_USER_PLAYER = 2;
        public const int COLOR_WALLS = 9;
        public const int COLOR_KEY_OUTER = 14;
        public const int COLOR_KEY_INNER = 6;
        public const int COLOR_TRAP_OUTER = 4;
        public const int COLOR_TRAP_INNER = 0;
        public const int COLOR_DOOR = 11;
        public const int COLOR_LEVEL_PORTAL = 9;
        public const int COLOR_NPC_PLAYER = 12;


        public const int COLOR_LOGO_INNER = 9;
        public const int COLOR_LOGO_OUTER = 0;
        public const string MAIN_LOGO = @"


              ___      _______  _______  __   __  ______    ___   __    _  _______  __   __   
             |   |    |   _   ||  _    ||  | |  ||    _ |  |   | |  |  | ||       ||  | |  |  
             |   |    |  |_|  || |_|   ||  |_|  ||   | ||  |   | |   |_| ||_     _||  |_|  |  
             |   |    |       ||       ||       ||   |_||_ |   | |       |  |   |  |       |  
             |   |___ |       ||  _   | |_     _||    __  ||   | |  _    |  |   |  |       |  
             |       ||   _   || |_|   |  |   |  |   |  | ||   | | | |   |  |   |  |   _   |  
             |_______||__| |__||_______|  |___|  |___|  |_||___| |_|  |__|  |___|  |__| |__|  
                _______  __   __  _______  ___      _______  ______    _______  ______        
               |       ||  |_|  ||       ||   |    |       ||    _ |  |       ||    _ |       
               |    ___||       ||    _  ||   |    |   _   ||   | ||  |    ___||   | ||       
               |   |___ |       ||   |_| ||   |    |  | |  ||   |_||_ |   |___ |   |_||_      
               |    ___| |     | |    ___||   |___ |  |_|  ||    __  ||    ___||    __  |     
               |   |___ |   _   ||   |    |       ||       ||   |  | ||   |___ |   |  | |     
               |_______||__| |__||___|    |_______||_______||___|  |_||_______||___|  |_|


";


        //MENU options Event to nice strings conversions consts
        public const string EVENT_GAME_STEP = "Resume Game";
        public const string EVENT_MENU_GAME_SUMMARY = "Game Summary";
        public const string EVENT_MENU_MAIN_PAUSED = "Game Paused Menu";
        public const string EVENT_MENU_NEW_GAME = "Main Menu";
        public const string EVENT_LEVEL_SUMMARY = "Level Complete";
        public const string EVENT_GAME_OVER = "Game Over";
        public const string EVENT_LEVEL_CHECK_NEXT_LEVEL = "Load Next Level";
        public const string EVENT_LEVEL_LOAD_NEXT = "Load Next Level";
        public const string EVENT_LEVEL_NEW_GAME = "Start New Game";
        public const string EVENT_LEVEL_RESTART_CURRENT_LEVEL = "Restart Level";
        public const string EVENT_UI_QUIT_GAME = "Quit Game";
        //public const string EVENT_MENU_QUIT_TO_MAIN_MENU = "Quit To Main Menu"; not implemented yet
    }                       
}

  
