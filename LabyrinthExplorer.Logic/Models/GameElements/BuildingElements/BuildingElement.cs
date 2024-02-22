using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements.BuildingElements
{
    public abstract class BuildingElement : GameElement
    {
        public bool Open { get; set; }
        public bool Locked { get; set; }
    }
}
