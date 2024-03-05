using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class UserPlayer : CharacterElement, IMove
    {
        public UserPlayer() 
        {
            Inventory = new List<ItemElement>();
        }
        public UserPlayer(int x, int y)
        {
            Inventory = new List<ItemElement>();
            Name = "User Player";
            Position = new Coordinates(x, y);
            Model = 'P';
        }     

       /* public InterActionDTO ReceiveInterActionDTO(InterActionDTO input)
        {
            InterActionDTO output = input;
            switch (input.InputAction)
            {
                case InputAction.Up:
                    output = MoveUp(input);
                    break;
                case InputAction.Right:
                    output = MoveRight(input);
                    break;
                case InputAction.Down:              //Only implemented
                    output = MoveDown(input);
                    break;
                case InputAction.Left:
                    output = MoveLeft(input);
                    break;
                default:
                    output.DTO.Message = "ERROR: UserPlayer.ReceiveInterActionDTO: Input not recognized";
                    output.DTO.Success = false;
                    output.DTO.Error = true;
                    break;
            }

            return output;
        }*/

    }

}
