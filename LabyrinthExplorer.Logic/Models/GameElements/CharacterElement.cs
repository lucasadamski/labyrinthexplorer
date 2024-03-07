using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static LabyrinthExplorer.Data.Helpers.Settings;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public abstract class CharacterElement : GameElement, IMove
    {
        public CharacterElement() { }
        public CharacterElement(int x, int y)
        {
            Position = new Coordinates(x, y);
            HiddenElement = new EmptySpace(x, y);
        }
        public byte Health { get; set; }
        public List<ItemElement> Inventory { get; set; }
        public GameElement HiddenElement { get; set; }

        virtual protected GameElement HideElement(GameElement element)
        {
            return element;
        }

        virtual protected GameElement UnhideElement()
        {
            return HiddenElement;
        }

        virtual protected void MoveOpration(InterActionDTO interAction, Coordinates primary, Coordinates target, GameElement itemToHide)
        {
            if(itemToHide is ItemElement)
            {
                GameElement temp = interAction.MapOfElements[primary.X][primary.Y];
                interAction.MapOfElements[primary.X][primary.Y] = UnhideElement();
                HiddenElement = new EmptySpace(target.X, target.Y); //TODO not good with position, its relative to local MapOfElements
                interAction.MapOfElements[target.X][target.Y] = temp;
                return;
            }
            else
            {
                GameElement temp = interAction.MapOfElements[primary.X][primary.Y];
                interAction.MapOfElements[primary.X][primary.Y] = UnhideElement();                
                HiddenElement = HideElement(interAction.MapOfElements[target.X][target.Y]);
                interAction.MapOfElements[target.X][target.Y] = temp;                
                return;
            }
        }


        virtual public InterActionDTO MoveDown(InterActionDTO input)
        {
            InterActionDTO output = input;
            DTO dtoOutput = new DTO();

            Coordinates primaryPosition = new Coordinates(1, 1);
            Coordinates targetPosition  = new Coordinates(2, 1);

            if (input.MapOfElements[2][1] is EmptySpace es)
            {
                MoveOpration(output, primaryPosition, targetPosition, es);
                Position.X++;                
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

                MoveOpration(output, primaryPosition, targetPosition, ie);
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
                    MoveOpration(output, primaryPosition, targetPosition, d);
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
                case InputAction.Down:              //Only implemented from Moves method family
                    output = MoveDown(input);
                    break;
                case InputAction.Left:
                    output = MoveLeft(input);
                    break;
                case InputAction.UseWeapon:
                    output = UseWeapon(input);
                    break;
                case InputAction.Use:
                    output = UseAction(input);
                    break;
                default:
                    output.DTO.Message += $"ERROR: CharacterElement.ReceiveInterActionDTO: Input: {input.InputAction} not recognized\n";
                    output.DTO.Success = false;
                    output.DTO.Error = true;
                    break;
            }

            return output;
        }

        override public DTO DoDamage(byte amountOfDamage)
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

        public InterActionDTO UseWeapon(InterActionDTO input)
        {
            InterActionDTO output = input;
            DTO dtoOutput = new DTO();
            //1. call doDamage on every item around player
            for (int i = 0; i < output.MapOfElements.Length; i++)
            { 
                for (int j = 0; j < output.MapOfElements[i].Length; j++)
                {
                    if (i == 1 && j == 1) continue; //skip this CharacterElement
                    dtoOutput = output.MapOfElements[i][j].DoDamage(WEAPON_DAMAGE);
                    if (dtoOutput.Success == true)
                    {
                        output.DTO.Message += dtoOutput.Message;
                    }
                }
            }

            return output;
        }

        public InterActionDTO UseAction(InterActionDTO input)
        {
            InterActionDTO output = input;
            DTO dtoOutput = new DTO();
            //1. call Use on every item around player
            for (int i = 0; i < output.MapOfElements.Length; i++)
            {
                for (int j = 0; j < output.MapOfElements[i].Length; j++)
                {
                    if (i == 1 && j == 1) continue; //skip this CharacterElement
                    dtoOutput = output.MapOfElements[i][j].Use(this);
                    if (dtoOutput.Success == true)
                    {
                        output.DTO.Message += dtoOutput.Message;
                    }
                }
            }

            return output;
        }
    }
}
