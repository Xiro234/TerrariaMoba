using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class GoldenhammerDanceEffect : Daze {
        public override string DisplayName { get => "Dance of the Goldenhammer"; }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public GoldenhammerDanceEffect() { }

        public GoldenhammerDanceEffect(float magnitude, int duration, bool canBeCleansed) : base(magnitude, duration, canBeCleansed) { }
    }
}