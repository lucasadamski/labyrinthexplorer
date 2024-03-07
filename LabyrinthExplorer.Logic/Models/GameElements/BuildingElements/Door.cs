using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Logic.DTOs;
using System;
using System.Collections.Generic;
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
            if (Locked == false)
            {
                Open = OpenCloseDoors(Open, output);
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
            SwitchModels(); //TODO now don;t work
            //log what happened
            if (currentState == true) dto.Message += $"{this.Name} has been closed";
            else dto.Message += $"{this.Name} has been opened";
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
                    dto.Message += $"{this.Name} unlocked with {potentialKey.Name}";
                    return false;                   
                }
            }
            dto.Message += $"{this.Name} cannot be unlocked without key";
            return true;
        }

    }
}
