using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class SealOfHephaesta : Ability {
        public SealOfHephaesta() : base("Seal of Hephaesta", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - flash his seal to aid allies.
        }
    }
}