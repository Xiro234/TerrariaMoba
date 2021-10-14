using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class PaladinsResolve : Ability {
        public PaladinsResolve() : base("Paladin's Resolve", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        public override void WhileActive() {
            
        }
    }
}