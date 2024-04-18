using LabyrinthExplorer.Data.Helpers;


namespace LabyrinthExplorer.Data.DTOs
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

        public void AppendActionMessage(string message)
        {
            this.Message += Settings.LOGGER_DEBUG + " " + Settings.LOGGER_ACTION + " " + message + "\n";
        }
        public void AppendDebugMessage(string message)
        {
            this.Message += Settings.LOGGER_DEBUG + " " + message + "\n";
        }

        public void AppendEditedMessage(string editedMessage)
        {
            this.Message += editedMessage;
        }

        public void AppendErrorMessage(string message)
        {
            this.Message += Settings.LOGGER_ERROR + " " + message + "\n";
        }
    }
}
