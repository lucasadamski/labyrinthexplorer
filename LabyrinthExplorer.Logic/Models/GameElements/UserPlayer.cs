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

        public InterActionDTO MoveDown(InterActionDTO input)
        {
            InterActionDTO output = input;
            if (input.MapOfElements[2][1] is EmptySpace es)
            {
                EmptySpace temp = es;
                output.MapOfElements[2][1] = input.MapOfElements[1][1];
                output.MapOfElements[1][1] = es;
                Position.X++;
                es.Position.X--;
                output.DTO.Message = $"UserPlayer.MoveDown: Sucessfuly moved down to an {es.Name}";
            }
            if (input.MapOfElements[2][1] is ItemElement ie)
            {
                ie.Pickup(this);

                output.MapOfElements[2][1] = input.MapOfElements[1][1];
                output.MapOfElements[1][1] = new EmptySpace(input.CenterPosition.X, input.CenterPosition.Y);
                Position.X++;
                output.DTO.Message = $"UserPlayer.MoveDown: Sucessfuly moved down to an {ie.Name}. Picked up: {ie.Name}";
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
