using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class Hammerfall : Ability {
        public Hammerfall() : base("Hammerfall", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - drop massive hammer that deals damage and stuns.
        }
    }
}