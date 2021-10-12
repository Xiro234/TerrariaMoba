using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Marie {
    public class TorrentialPendant : Ability {
        public TorrentialPendant() : base("Torrential Pendant", 60, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void WhileActive() {
            //TODO - Figure out how it should work; causes rain, stats boosted whilst in water/rain.
        }
    }
}