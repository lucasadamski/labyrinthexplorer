using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements.BuildingElements
{
    public class Door : BuildingElement
    {
        public Door() 
        {
            Name = Settings.NAME_DOOR;
            Model = Settings.MODEL_CLOSED_DOOR;
            AlternateModel = Settings.MODEL_OPEN_DOOR;
        }
        public Door(int x, int y, bool open = false, bool locked = false)
        {
            Model = Settings.MODEL_CLOSED_DOOR;
            AlternateModel = Settings.MODEL_OPEN_DOOR;
            Open = open;
            Locked = locked;           

            if (Open == true) SwitchModels();          

            Name = Settings.NAME_DOOR;
            Position = new Coordinates(x, y);            
        }      
        override public DTO Use(CharacterElement player)
        {
            //if locked
            //if closed unlocked
            //if opened
            DTO output = new DTO();
            output.AppendActionMessage($"{player.Name} used {this.Name}");
            if (Locked == false)
            {
                Open = OpenCloseDoors(Open, output);
                string opened = "opened";
                string closed = "closed";
                output.Message += $"{player.Name} {(Open ? opened : closed )} {this.Name}"; //TODO
            }
            else
            {
                Locked = TryUnlockDoors(player.Inventory, output);
                if (Locked == false) Open = OpenCloseDoors(Open, output);
            }
            return output;
        }
        private bool OpenCloseDoors(bool currentState, DTO dto)
        {
            SwitchModels();
            //log what happened
            if (currentState == true) dto.AppendActionMessage($"{this.Name} has been closed");
            else dto.AppendActionMessage($"{this.Name} has been opened");
            //return oposite state
            return !currentState;
        }
        private bool TryUnlockDoors(List<ItemElement> charactersInventory, DTO dto)
        {
            //see if inventory contains key
            //if yes remove key and unlock doors
            //if no do nothing
            foreach (ItemElement item in charactersInventory)
            {
                if (item is Key k)
                {
                    Key potentialKey = k;
                    charactersInventory.Remove(potentialKey);
                    dto.AppendActionMessage($"{this.Name} unlocked with {potentialKey.Name}");
                    return false;                   
                }
            }
            dto.AppendActionMessage($"{this.Name} cannot be unlocked without key");
            return true;
        }

        override public DTO ReceiveStep(CharacterElement player)
        {
            DTO output = new DTO();
            if (Open == true)
            {
                output.AppendDebugMessage($"{this.Name} is open. {player.Name} allowed to step in");
                return output;
            }
            else
            {
                output.Success = false;
                output.AppendActionMessage($"{this.Name} is closed. {player.Name} not allowed to step in");
                return output;
            }
        }
    }
}
