using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class Frostbite : Ability {
        public Frostbite() : base("Frostbite", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Old Man reaches to his back pocket and flings 3 Frost Daggerfish in front of him, dealing damage and freezing.
        }
    }
}