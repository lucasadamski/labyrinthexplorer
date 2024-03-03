using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.DTOs
{
    public class DTO
    {
        public bool Success { get; set; } = true;
        public bool Error { get; set; } = false;
        public string Message { get; set; } = "";
        public DTO(bool success)
        {
            Success = success;
        }
        public DTO()
        {
            Success = true;
        }
        public DTO(string message)
        {
            Success = false;
            Error = true;
            Message = new string(message);
        }
    }
}
