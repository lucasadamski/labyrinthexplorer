using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic;
using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Models;
using System.Text;
using static System.Console;
using static LabyrinthExplorer.Data.Helpers.Settings;
using LabyrinthExplorer.Data.DTOs; //configuration file mock

namespace LabyrinthExplorer.ConsoleUI
{
    public class ConsoleUI
    {
        public GameEngine GE { get; set; }
        public GameEngineInputDTO Input { get; set; }
        public GameEngineOutputDTO Output { get; set; }

        private ConsoleKey userKeyPressed;

        private bool isFirstGameStep = true;

        public ConsoleUI()
        {
            char P = Settings.MODEL_USER_PLAYER;
            char[][] map = new char[10][]
              {
                  //                      0    1    2    3   4   5   6   7   8   9
                          new char[10] { '+', '-', 'F', '-','-','-','-','-','-','+' } //0
                        , new char[10] { '|',  P , ' ', '|',' ',' ',' ',' ','K','|' } //1
                        , new char[10] { '|', ' ', ' ', 'D',' ',' ',' ','X',' ','|' } //2
                        , new char[10] { '|', ' ', ' ', '|',' ',' ',' ',' ',' ','|' } //3
                        , new char[10] { '|', '-', '-', '+','-','-','-','L','-','|' } //4
                        , new char[10] { '|', ' ', ' ', ' ',' ',' ',' ',' ',' ','|' } //5
                        , new char[10] { '|', ' ', ' ', 'W',' ',' ',' ',' ',' ','|' } //6
                        , new char[10] { '|', ' ', ' ', ' ',' ',' ','E',' ',' ','|' } //7
                        , new char[10] { '|', ' ', ' ', ' ',' ',' ',' ',' ',' ','|' } //8
                        , new char[10] { '+', '-', '-', '-','-','-','-','-','-','+' } //9
              };

            //GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            GE = new GameEngine(Settings.ALL_LEVELS);
            Input = new GameEngineInputDTO();
            Output = new GameEngineOutputDTO();
        }


        public bool RunGameStep()
        {
            if (isFirstGameStep)
            {
                DrawLogo();
                isFirstGameStep = false;
            }
            if (!Output.IsApplicationActive) //quits game
            {
                Clear();
                return Output.IsApplicationActive; //return false, so Program.cs exits the while loop
            }
            //bool is first run -> play video lucada software, audio
            userKeyPressed = ReadKey().Key;
            Input = PrepareGameEngineInputDTO(userKeyPressed);
            Output = GE.RunEngine(Input); //makes the Engine do the magic
            ClearScreenBelowPoint(HEIGHT_LOGO);
            
            if (Output.IsGameActive) //draws game
            {
                ClearWholeScreen();
                DrawFrame(Output.Frame);
                DrawHUD(Output.HUD);
                DrawLog(Output.Log);
                return Output.DTO.Success;
            }
            else   //draws menu only
            {
                ClearWholeScreen();
                DrawLogo();
                DrawMenu(Output.Menu);
                DrawLog(Output.Log);
                return Output.DTO.Success;
            }
        }
        private void DrawMenu(MenuDTO menu)
        {
            WriteLine("\n\n\t\t\t\t\t" + menu.Title.ToUpper() + "\n");
            for (int i = 0; i < menu.Options.Count(); i++)
            {
                if (menu.ActiveOptionIndex == i)
                {
                    SwitchMenuActiveOptionIndex();
                    WriteLine("\t\t\t\t\t" + menu.Options.ElementAt(i));
                    SwitchMenuActiveOptionIndex();
                    continue;
                }
                WriteLine("\t\t\t\t\t" + menu.Options.ElementAt(i));
            }
        }

        private void DrawFrame(char[][] frame)
        {
            int sizeX = frame.Length;
            int sizeY = frame[0].Length;

            //converts char[][] to string
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    output = output.Append(frame[i][j]);
                }
                output = output.Append("\n");
            }
            //WriteLine(output.ToString());
            foreach (char character in output.ToString())
            {
                switch(character)
                {
                    case ' ':
                        break;
                    case '-':
                    case '|':
                    case '+':
                        ForegroundColor = (ConsoleColor)COLOR_WALLS;
                        break;
                    case 'D':
                    case 'O':
                        ForegroundColor = (ConsoleColor)COLOR_DOOR;
                        break;
                    case 'X':
                        BackgroundColor = (ConsoleColor)COLOR_TRAP_OUTER;
                        ForegroundColor = (ConsoleColor)COLOR_TRAP_INNER;
                        break;
                    case 'P':
                        BackgroundColor = (ConsoleColor)COLOR_USER_PLAYER;
                        ForegroundColor = (ConsoleColor)COLOR_USER_PLAYER;
                        break;
                    case 'K':
                        ForegroundColor = (ConsoleColor)COLOR_KEY_INNER;
                        BackgroundColor = (ConsoleColor)COLOR_KEY_OUTER;
                        break;
                    case 'E':
                        BackgroundColor = (ConsoleColor)COLOR_NPC_PLAYER;
                        ForegroundColor = (ConsoleColor)COLOR_NPC_PLAYER;
                        break;
                };
                Write(character);
                ForegroundColor = ConsoleColor.Gray;
                BackgroundColor = ConsoleColor.Black;
            }
            WriteLine($"SPACEBAR - Use    C - UseWeapon   ESCAPE - Exit");
        }
        private void DrawLog(string log)
        {
            if (CONSOLE_UI_DISPLAY_DEBUG_LOG)
            {
                WriteLine("********\nDebug Log:");
                WriteLine(log);
            }
        }
        private void DrawHUD(string hud)
        {
            //WriteLine(hud);
            foreach (string line in hud.Split('\n'))
            {
                Write("\r" + new string(' ', WindowWidth) + "\r");
                WriteLine(line);
            }
        }
        private void DrawLogo()
        {
            foreach (char character in MAIN_LOGO)
            { 
                if (character == '|')
                {
                    Write(character);
                    SwitchLogoInnerColor();
                }
                else
                {
                    Write(character);
                }
            }
        }
        private void SwitchLogoInnerColor()
        {
            if (BackgroundColor == (ConsoleColor)COLOR_LOGO_INNER)
            {
                BackgroundColor = (ConsoleColor)COLOR_LOGO_OUTER;
            }
            else
            {
                BackgroundColor = (ConsoleColor)COLOR_LOGO_INNER;
            }
        }
        private void SwitchMenuActiveOptionIndex()
        {
            if (BackgroundColor == (ConsoleColor)COLOR_LOGO_INNER)
            {
                BackgroundColor = (ConsoleColor)COLOR_LOGO_OUTER;
            }
            else
            {
                BackgroundColor = (ConsoleColor)COLOR_LOGO_INNER;
            }
        }
        private GameEngineInputDTO PrepareGameEngineInputDTO(ConsoleKey input)
        {
            return new GameEngineInputDTO()
            {
                DTO = new DTO(),
                InputAction = ParseUIInputToGameEngineInputAction(input)
            };
        }
        private InputAction ParseUIInputToGameEngineInputAction(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    return InputAction.Up;
                case ConsoleKey.DownArrow:
                    return InputAction.Down;
                case ConsoleKey.LeftArrow:
                    return InputAction.Left;
                case ConsoleKey.RightArrow:
                    return InputAction.Right;
                case ConsoleKey.Spacebar:
                    return InputAction.Use;
                case ConsoleKey.Escape:
                    return InputAction.ExitToMenu;
                case ConsoleKey.C:
                    return InputAction.UseWeapon;
                default:
                    return InputAction.Unknown;
            }
        }
        private void ClearWholeScreen()
        {
            if (CONSOLE_UI_DISPLAY_CLEAR_SCREEN_PER_FRAME)
            {
                Clear();
                //SetCursorPosition(0, 18);
            }
        }
        private void ClearScreenBelowPoint(int heightPoint)
        {
            if (CONSOLE_UI_DISPLAY_CLEAR_SCREEN_PER_FRAME)
            {
                SetCursorPosition(0, heightPoint + 1);
                for (int i = (WindowHeight - (heightPoint + 2)); i > 0; i--)
                {
                    WriteLine(new string(' ', WindowWidth));
                }
                SetCursorPosition(0, heightPoint + 1);
            }
        }

    }
}
