using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public abstract class GameElement
    {
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public bool Visible { get; set; }
        public char Model { get; set; }
        public char AlternateModel { get; set; }
        public bool MoveThrough { get; set; }
    }
}
