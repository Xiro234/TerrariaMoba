using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class BrewConcoction : Ability {
        public BrewConcoction() : base("Brew Concoction", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Chastradamus can brew different effects into his Toxic Flask from his Concoction Pool.
        }

        public override void WhileActive() {
            
        }
    }
}