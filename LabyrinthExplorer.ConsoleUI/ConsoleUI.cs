using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic;
using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LabyrinthExplorer.ConsoleUI
{
    public class ConsoleUI
    {
        public GameEngine GE { get; set; }
        public GameEngineInputDTO Input { get; set; }
        public GameEngineOutputDTO Output { get; set; }

        private ConsoleKey userKeyPressed;

        public ConsoleUI()
        {
            char P = Settings.MODEL_USER_PLAYER;
            char[][] map = new char[10][]
              {
                  //                      0    1    2    3   4   5   6   7   8   9
                          new char[10] { '+', '-', '-', '-','-','-','-','-','-','+' } //0
                        , new char[10] { '|',  P , ' ', '|',' ',' ',' ',' ','K','|' } //1
                        , new char[10] { '|', ' ', ' ', 'D',' ',' ',' ','X',' ','|' } //2
                        , new char[10] { '|', ' ', ' ', '|',' ',' ',' ',' ',' ','|' } //3
                        , new char[10] { '|', '-', '-', '+','-','-','-','L','-','|' } //4
                        , new char[10] { '|', ' ', ' ', ' ',' ',' ',' ',' ',' ','|' } //5
                        , new char[10] { '|', ' ', ' ', ' ',' ',' ',' ',' ',' ','|' } //6
                        , new char[10] { '|', ' ', ' ', ' ',' ',' ','E',' ',' ','|' } //7
                        , new char[10] { '|', ' ', ' ', ' ',' ',' ',' ',' ',' ','|' } //8
                        , new char[10] { '+', '-', '-', '-','-','-','-','-','-','+' } //9
              };

            GE = new GameEngine(Settings.INJECTED_LEVEL, map);
            Input = new GameEngineInputDTO();
            Output = new GameEngineOutputDTO();
        }


        public bool RunGameStep()
        {
            userKeyPressed = ReadKey().Key;
            Input = PrepareGameEngineInputDTO(userKeyPressed);
            Clear();
            Output = GE.RunEngine(Input);
            DrawFrame(Output.Frame);
            DrawHUD(Output.HUD);
            DrawLog(Output.Log);
            return Output.DTO.Success;
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
            //ConsoleUI display
            WriteLine("L A B Y R I N T H      E X P L O R E R");
            WriteLine(output.ToString());
            WriteLine($"SPACEBAR - Use    C - UseWeapon   ESCAPE - Exit");
        }
        private void DrawLog(string log)
        {
            WriteLine("********\nDebug Log:");
            WriteLine(log);
        }
        private void DrawHUD(string hud)
        {
            WriteLine(hud);
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
    }
}
