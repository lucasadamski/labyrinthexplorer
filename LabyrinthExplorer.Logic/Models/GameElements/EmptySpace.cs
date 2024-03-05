using LabyrinthExplorer.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class EmptySpace : GameElement
    {
        public EmptySpace() 
        {
            Name = Settings.NAME_EMPTY_SPACE;
            Model = Settings.MODEL_EMPTY_SPACE;
        }
        public EmptySpace(int x, int y)
        {
            Name = Settings.NAME_EMPTY_SPACE;
            Model = Settings.MODEL_EMPTY_SPACE;
            Position = new Coordinates(x, y);
        }
    }
}
