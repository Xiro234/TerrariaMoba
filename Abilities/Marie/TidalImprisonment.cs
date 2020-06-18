using System;
using Terraria;

namespace TerrariaMoba.Abilities.Marie {
    public class TidalImprisonment : Ability {
        public TidalImprisonment(Player myPlayer) : base(myPlayer) {
            Name = "Tidal Imprisonment";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieUltimateTwo");
        }
        
        public override void OnCast() {
            
        }
    }
}