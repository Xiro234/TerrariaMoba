using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class Hammerfall : Ability {
        public Hammerfall() : base("Hammerfall", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - drop massive hammer that deals damage and stuns.
        }
    }
}