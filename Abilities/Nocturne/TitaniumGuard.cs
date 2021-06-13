using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class TitaniumGuard : Ability {
        public TitaniumGuard() : base("Titanium Guard", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Nocturne takes a quick guard stance to reflect incoming projectiles.
        }
    }
}