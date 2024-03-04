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
        public UserPlayer() { }
        public UserPlayer(int x, int y)
        {
            Name = "User Player";
            Position = new Coordinates(x, y);
            Model = 'P';
        }

        public InterActionDTO MoveDown(InterActionDTO input)
        {
            InterActionDTO output = input;
            if (input.MapOfElements[2][1] is EmptySpace e)
            {
                EmptySpace temp = e;
                output.MapOfElements[2][1] = input.MapOfElements[1][1];
                output.MapOfElements[1][1] = e;
                Position.X++;
                e.Position.X--;
                output.DTO.Message = "UserPlayer.MoveDown: Sucessfuly moved down to an empty space";
            }
            
            return output;
        }

        public InterActionDTO MoveLeft(InterActionDTO input)
        {
            throw new NotImplementedException();
        }

        public InterActionDTO MoveRight(InterActionDTO input)
        {
            throw new NotImplementedException();
        }

        public InterActionDTO MoveUp(InterActionDTO input)
        {
            throw new NotImplementedException();
        }

        public InterActionDTO ReceiveInterActionDTO(InterActionDTO input)
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
        }

    }

}
