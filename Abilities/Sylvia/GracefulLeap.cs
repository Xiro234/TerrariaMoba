using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Sylvia {
    public class GracefulLeap : Ability {
        public GracefulLeap() : base("Graceful Leap", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank");  }

        public override void OnCast() {
            //TODO - Leap into the air and fire 5 auto-attack arrows below her in a fan arc.
        }
    }
}