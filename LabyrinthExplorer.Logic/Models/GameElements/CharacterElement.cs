using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public abstract class CharacterElement : GameElement
    {
        public CharacterElement() { }
        public CharacterElement(int x, int y)
        {
            Position = new Coordinates(x, y);
        }
        public byte Health { get; set; }
        public List<ItemElement> Inventory { get; set; }

    }
}
