using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public abstract class CharacterElement : GameElement, IMove
    {
        public CharacterElement() { }
        public CharacterElement(int x, int y)
        {
            Position = new Coordinates(x, y);
        }
        public byte Health { get; set; }
        public List<ItemElement> Inventory { get; set; }

        virtual public InterActionDTO MoveDown(InterActionDTO input)
        {
            InterActionDTO output = input;
            if (input.MapOfElements[2][1] is EmptySpace es)
            {
                EmptySpace temp = es;
                output.MapOfElements[2][1] = input.MapOfElements[1][1];
                output.MapOfElements[1][1] = es;
                Position.X++;
                es.Position.X--;
                output.DTO.Message = $"CharacterElement.MoveDown: Sucessfuly moved down {Name} to an {es.Name}";
            }
            if (input.MapOfElements[2][1] is ItemElement ie)
            {
                ie.Pickup(this);

                output.MapOfElements[2][1] = input.MapOfElements[1][1];
                output.MapOfElements[1][1] = new EmptySpace(input.CenterPosition.X, input.CenterPosition.Y);
                Position.X++;
                output.DTO.Message = $"CharacterElement.MoveDown: Sucessfuly moved down {Name} to an {ie.Name}. Picked up: {ie.Name}";
            }

            return output;
        }

        virtual public InterActionDTO MoveLeft(InterActionDTO input)
        {
            return input;
        }

        virtual public InterActionDTO MoveRight(InterActionDTO input)
        {
            return input;
        }

        virtual public InterActionDTO MoveUp(InterActionDTO input)
        {
            return input;
        }

        virtual public InterActionDTO ReceiveInterActionDTO(InterActionDTO input)
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
                    output.DTO.Message = $"ERROR: CharacterElement.ReceiveInterActionDTO: Input: {input.InputAction} not recognized";
                    output.DTO.Success = false;
                    output.DTO.Error = true;
                    break;
            }

            return output;
        }
    }
}
