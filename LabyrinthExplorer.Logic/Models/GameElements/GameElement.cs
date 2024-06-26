﻿using LabyrinthExplorer.Logic.Infrastructure;
using LabyrinthExplorer.Data.DTOs;

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


        virtual public void SwitchModels()
        {
            char temp = Model;
            Model = AlternateModel;
            AlternateModel = temp;
        }
        virtual public DTO DoDamage(CharacterElement playerDoneDamageTo) => new DTO("Not implemented\n", false);
        virtual public DTO DoDamage(byte amountOfDamage) => new DTO("Not implemented\n", false);      
        virtual public DTO Pickup(CharacterElement player) => new DTO("Not implemented\n", false);     
        virtual public DTO Use(CharacterElement player) => new DTO("Not implemented\n", false);
        virtual public DTO ReceiveStep(CharacterElement player) => new DTO($"{this.Name} cannot be stepped on by {player.Name}\n", false);
        virtual public DTO Pickup(ItemElement item) => new DTO("Not implemented\n", false);
        
    }
}
