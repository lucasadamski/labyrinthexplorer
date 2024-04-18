using LabyrinthExplorer.Data.DTOs;

/*
 * Canvas Symbols Game Engine -> UI Translation:
    • @ -User Player
    • D - Unlocked Closed Door
    • O - Open Door
    • L - Locked Closed Door
    • - - Horizontal Wall
    • | - Veritcal Wall
    • + - Corner Wall
    • X - Trap
    • $ - Enemy
    • K - Key
    • W - Weapon
*/
namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class Level
    {
        public DTO DTO { get; set; } = new DTO();
        public Coordinates Size { get; set; }
        public char[][] Map { get; set; }
        public string Name { get; set; }
    }
}
