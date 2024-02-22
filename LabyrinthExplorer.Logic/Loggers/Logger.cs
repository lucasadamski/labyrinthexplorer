using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Loggers
{
    public class Logger
    {
        public StringBuilder Message { get; set; } = new StringBuilder();
        private uint lineNumber = 0;
        public void ClearMessage()
        {
            Message = Message.Clear();
        }
        public void Log(string message)
        {
            Message = Message.AppendLine(lineNumber++.ToString() + ": " + message);
        }

        public void LogError(string message)
        {
            Message = Message.AppendLine("***ERROR*** " + lineNumber++.ToString() + ": " + message);
        }


    }
}
