using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using LabyrinthExplorer.Logic.Models.GameElements.BuildingElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
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
        public sbyte Health { get; set; }
        public List<ItemElement> Inventory { get; set; }
        public GameElement HiddenElement { get; set; }
        public bool IsLevelFinished { get; set; } = false;

        virtual protected GameElement HideElement(GameElement element)
        {
            element.NotVisible = true;
            return element;
        }

        virtual protected GameElement UnhideElement()
        {
            HiddenElement.NotVisible = false;
            return HiddenElement;
        }

        virtual protected void MoveOperation(InterActionDTO interAction, Coordinates primary, Coordinates target, GameElement itemToHide)
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


        virtual public InterActionDTO MoveDown(InterActionDTO input)    => Move(input, new Coordinates(2, 1));
        virtual public InterActionDTO MoveLeft(InterActionDTO input)    => Move(input, new Coordinates(1, 0));
        virtual public InterActionDTO MoveRight(InterActionDTO input)   => Move(input, new Coordinates(1, 2));
        virtual public InterActionDTO MoveUp(InterActionDTO input)      => Move(input, new Coordinates(0, 1));

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
                    output.DTO.AppendErrorMessage($"CharacterElement.ReceiveInterActionDTO: Input: {input.InputAction} not recognized");
                    output.DTO.Success = false;
                    output.DTO.Error = true;
                    break;
            }

            return output;
        }

        override public DTO DoDamage(byte amountOfDamage)
        {
            DTO output = new DTO();
            Health -= (sbyte)amountOfDamage;
            output.AppendActionMessage($"{this.Name} took {amountOfDamage} damage");
            if (Health < 1)
            {
                NotVisible = true; //End of game in case of UserPlayer
                output.AppendActionMessage($"{this.Name} is dead");
            }
            return output;
        }

        public InterActionDTO UseWeapon(InterActionDTO input)
        {
            InterActionDTO output = input;
            DTO dtoOutput = new DTO();
            output.DTO.AppendActionMessage($"{this.Name} used Weapon");
            //1. call doDamage on every item around player
            for (int i = 0; i < output.MapOfElements.Length; i++)
            { 
                for (int j = 0; j < output.MapOfElements[i].Length; j++)
                {
                    if (i == 1 && j == 1) continue; //skip this CharacterElement
                    dtoOutput = output.MapOfElements[i][j].DoDamage(WEAPON_DAMAGE);
                    if (dtoOutput.Success == true)
                    {
                        output.DTO.AppendActionMessage($"{this.Name} done {WEAPON_DAMAGE} damage to {output.MapOfElements[i][j].Name}");
                        output.DTO.AppendEditedMessage(dtoOutput.Message);
                        if (output.MapOfElements[i][j].NotVisible == true)
                        {
                            output.MapOfElements[i][j] = new EmptySpace(output.MapOfElements[i][j].Position.X, output.MapOfElements[i][j].Position.Y); //turn corpse into an empty space
                        }
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
                        output.DTO.AppendEditedMessage(dtoOutput.Message);
                    }
                }
            }

            return output;
        }

        override public DTO Pickup(ItemElement item)
        {
            DTO output = new DTO();
            Inventory.Add(item);
            output.AppendActionMessage($"{this.Name} picked up {item.Name}\n");
            return output;
        }

        protected InterActionDTO Move(InterActionDTO input, Coordinates targetPosition)
        {
            InterActionDTO output = input;
            DTO dtoOutput = new DTO();

            Coordinates primaryPosition = new Coordinates(1, 1);
            GameElement targetElement = input.MapOfElements[targetPosition.X][targetPosition.Y];
            int targetX = targetPosition.X;
            int targetY = targetPosition.Y;

            dtoOutput = targetElement.ReceiveStep(this);
            if (dtoOutput.Success == true) //reacts to step
            {
                if (targetElement is NPCPlayer)
                {
                    output.DTO.AppendEditedMessage(dtoOutput.Message);
                    return output;     //step denied, return output without MoveOperation
                }
                if (targetElement is Door d)
                {
                    if (d.Open == false)
                    {
                        output.DTO.AppendDebugMessage($"CharacterElement.Move: Move Denied. {Name} can not move to {d.Name}");
                        return output;    //step denied, return output without MoveOperation
                    }
                    else if (d.Open == true) //allows to step on itself
                    {
                        output.DTO.AppendDebugMessage($"CharacterElement.Move: Sucessfuly moved {Name} to an Open {d.Name}");
                    }
                    output.DTO.AppendEditedMessage(dtoOutput.Message);
                }
                MoveOperation(output, primaryPosition, targetPosition, targetElement);
                UpdatePrimaryAndTargetElementsPositions(targetElement, targetX, targetY);

                output.DTO.AppendEditedMessage(dtoOutput.Message);
                return output;
            }
            else
            {
                output.DTO.AppendEditedMessage(dtoOutput.Message);
                return output;
            }
        }

        private void UpdatePrimaryAndTargetElementsPositions(GameElement targetElement, int targetX, int targetY)
        {
            if (targetX == 0 && targetY == 1) //move up
            {
                Position.X--; //update Player position
                targetElement.Position.X++; //update target Element position
            }
            if (targetX == 1 && targetY == 2) //move right
            {
                Position.Y++;
                targetElement.Position.Y--;
            }
            if (targetX == 2 && targetY == 1) //move down
            {
                Position.X++;
                targetElement.Position.X--;
            }
            if (targetX == 1 && targetY == 0) //move left
            {
                Position.Y--;
                targetElement.Position.Y++;
            }
        }
    }
}
