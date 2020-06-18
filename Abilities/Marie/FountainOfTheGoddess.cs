using System;
using Terraria;

namespace TerrariaMoba.Abilities.Marie {
    public class FountainOfTheGoddess : Ability {
        public FountainOfTheGoddess(Player myPlayer) : base(myPlayer) {
            Name = "Fountain of the Goddess";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieUltimateOne");
        }
        
        public override void OnCast() {
            
        }
    }
}