using System;
using Terraria;

namespace TerrariaMoba.Abilities.Marie {
    public class TomeOfLacusia : Ability {
        public TomeOfLacusia(Player myPlayer) : base(myPlayer) {
            Name = "Tome of Lacusia";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieAbilityTwo");
        }

        public override void OnCast() {
            
        }
    }
}