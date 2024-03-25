using LabyrinthExplorer.Logic.DTOs.InternalGameEngineDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Managers
{
    internal class Manager
    {
        public virtual InternalDTO ReceiveInternalDTO(InternalDTO inputDTO) => inputDTO;


    }
}
