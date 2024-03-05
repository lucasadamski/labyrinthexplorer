using LabyrinthExplorer.Data.Helpers;
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
            Model = Settings.MODEL_CLOSED_DOOR;
        }
        public Door(int x, int y)
        {
            Name = "Unlocked Closed Door";
            Position = new Coordinates(x, y);
            Model = Settings.MODEL_CLOSED_DOOR;
        }
        public Door(bool locked)
        {            
            Locked = locked;
            Model = Settings.MODEL_CLOSED_DOOR;
        }
    }
}
