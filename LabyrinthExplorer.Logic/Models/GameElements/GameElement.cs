using LabyrinthExplorer.Logic.DTOs;
using LabyrinthExplorer.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthExplorer.Logic.Models.GameElements
{
    public abstract class GameElement : IInteract
    {
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public bool NotVisible { get; set; }
        public char Model { get; set; }
        public char AlternateModel { get; set; }
        public bool MoveThrough { get; set; }

        virtual public DTO DoDamage(CharacterElement playerDoneDamageTo) => new DTO("Not implemented\n", false);
        virtual public DTO DoDamage(byte amountOfDamage) => new DTO("Not implemented\n", false);      
        virtual public DTO Pickup(CharacterElement player) => new DTO("Not implemented\n", false);     
        virtual public DTO Use(CharacterElement player) => new DTO("Not implemented\n", false);
        
    }
}
