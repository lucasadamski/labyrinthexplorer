using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.DTOs;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class Key : ItemElement
    {
        public Key() 
        {
            Name = Settings.NAME_KEY;
            Model = Settings.MODEL_KEY;
        }
        public Key(int x, int y) 
        {
            Name = Settings.NAME_KEY;
            Model = Settings.MODEL_KEY;
            Position = new Coordinates(x, y);
        }
       

        override public DTO ReceiveStep(CharacterElement player) => Pickup(player);
    }
}
