using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public abstract class CharacterElement : GameElement, IMove, IInteract
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
            DTO dtoOutput = new DTO();

            if (input.MapOfElements[2][1] is EmptySpace es)
            {
                EmptySpace temp = es;
                output.MapOfElements[2][1] = input.MapOfElements[1][1];
                output.MapOfElements[1][1] = es;
                Position.X++;
                es.Position.X--;
                output.DTO.Message += $"CharacterElement.MoveDown: Sucessfuly moved down {Name} to an {es.Name}\n";
                return output;
            }
            else if (input.MapOfElements[2][1] is Wall w)
            {
                output.DTO.Message += $"CharacterElement.MoveDown: Move Down Denied. {Name} can not move to {w.Name}\n";
                return output;
            }
            else if (input.MapOfElements[2][1] is ItemElement ie)
            {
                dtoOutput = ie.Pickup(this);
                output.DTO.Message += dtoOutput.Message;

                output.MapOfElements[2][1] = input.MapOfElements[1][1];
                output.MapOfElements[1][1] = new EmptySpace(input.CenterPosition.X, input.CenterPosition.Y);
                Position.X++;
                output.DTO.Message += $"CharacterElement.MoveDown: Sucessfuly moved down {Name} to an {ie.Name}\n";
                return output;
            }
            else if (input.MapOfElements[2][1] is Door d)
            {
                if (d.Open == false)
                {
                    output.DTO.Message += $"CharacterElement.MoveDown: Move Down Denied. {Name} can not move to {d.Name}\n";
                    return output;
                }
                else if (d.Open == true)
                {
                    output.MapOfElements[2][1] = input.MapOfElements[1][1];
                    output.MapOfElements[1][1] = new EmptySpace(input.CenterPosition.X, input.CenterPosition.Y);
                    Position.X++;
                    output.DTO.Message += $"CharacterElement.MoveDown: Sucessfuly moved down {Name} to an Open {d.Name}\n";
                    return output;
                }
                else
                    return output;
                
            }
            else if (input.MapOfElements[2][1] is NPCPlayer npc)
            {
                dtoOutput = npc.DoDamage(this);
                output.DTO.Message += dtoOutput.Message;
                output.DTO.Message += $"CharacterElement.MoveDown: Move Down Denied. {Name} can not move to {npc.Name}\n";
                return output;
            }
            else
            {
                return output;
            }
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
                    output.DTO.Message += $"ERROR: CharacterElement.ReceiveInterActionDTO: Input: {input.InputAction} not recognized\n";
                    output.DTO.Success = false;
                    output.DTO.Error = true;
                    break;
            }

            return output;
        }

        virtual public DTO DoDamage(byte amountOfDamage)
        {
            DTO output = new DTO();
            Health -= amountOfDamage;
            output.Message += $"{this.Name} took {amountOfDamage} damage\n";
            if (Health < 1)
            {
                NotVisible = true; //End of game in case of UserPlayer
                output.Message += $"{this.Name} is dead.\n";
            }
            return output;
        }

        virtual public DTO DoDamage(CharacterElement playerDoneDamageTo) => new DTO("Not implemented\n", false);
        virtual public DTO UseDoor(CharacterElement player) => new DTO("Not implemented\n", false);
        virtual public DTO Pickup(CharacterElement player) => new DTO("Not implemented\n", false);
    }
}
