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
            Name = Settings.NAME_CLOSED_DOOR;
            Model = Settings.MODEL_CLOSED_DOOR;
        }
        public Door(int x, int y)
        {
            Name = Settings.NAME_CLOSED_DOOR;
            Position = new Coordinates(x, y);
            Model = Settings.MODEL_CLOSED_DOOR;
        }
        public Door(bool locked)
        {            
            Locked = locked;
            Model = Settings.MODEL_CLOSED_DOOR;
            Name = Settings.NAME_CLOSED_DOOR;
        }
    }
}
