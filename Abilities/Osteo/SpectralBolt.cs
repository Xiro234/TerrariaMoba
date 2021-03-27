using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Osteo {
    public class SpectralBolt : Ability {
        public SpectralBolt() : base("Spectral Bolt", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Osteo fires a ghastly projectile that homes in on his enemies and deals damage.
        }
    }
}