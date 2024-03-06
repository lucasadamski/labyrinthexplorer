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
        public DTO() { }
        public DTO(string message, bool success = true, bool error = false)
        {
            Success = success;
            Error = error;
            Message = message;
        }
        public DTO(bool success)
        {
            Success = success;
        }
    }
}
