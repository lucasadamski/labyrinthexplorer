using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Infrastructure
{
    public interface IMove
    {
        public bool MoveUp(GameElement element);
        public bool MoveDown(GameElement element);
        public bool MoveLeft(GameElement element);
        public bool MoveRight(GameElement element);
    }
}
