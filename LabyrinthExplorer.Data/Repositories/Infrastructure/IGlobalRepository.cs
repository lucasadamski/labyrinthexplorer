using LabyrinthExplorer.Logic.Models.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Data.Repositories.Infrastructure
{
    public interface IGlobalRepository
    {
        public Level GetLevel(string name);
    }
}
