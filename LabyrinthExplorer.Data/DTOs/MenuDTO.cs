
namespace LabyrinthExplorer.Data.DTOs
{
    public class MenuDTO
    {
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public List<string> Options { get; set; } = new List<string>();
        public int ActiveOptionIndex { get; set; } = 0;
    }
}
