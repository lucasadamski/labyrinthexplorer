using LabyrinthExplorer.Data.Helpers;
using LabyrinthExplorer.Data.DTOs;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public class EmptySpace : GameElement
    {
        public EmptySpace() 
        {
            Name = Settings.NAME_EMPTY_SPACE;
            Model = Settings.MODEL_EMPTY_SPACE;
        }
        public EmptySpace(int x, int y)
        {
            Name = Settings.NAME_EMPTY_SPACE;
            Model = Settings.MODEL_EMPTY_SPACE;
            Position = new Coordinates(x, y);
        }

        override public DTO ReceiveStep(CharacterElement player)
        {
            NotVisible = true;
            return new DTO();
        }
    }
}
