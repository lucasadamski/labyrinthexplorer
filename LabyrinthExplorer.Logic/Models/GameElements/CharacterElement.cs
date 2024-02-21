using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public abstract class CharacterElement : GameElement
    {
        public byte Health { get; set; }
        public List<ItemElement> Inventory { get; set; }
    }
}
