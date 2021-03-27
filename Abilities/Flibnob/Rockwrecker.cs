using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Flibnob {
    public class Rockwrecker : Ability {
        public Rockwrecker() : base("Rockwrecker", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Flibnob slams his axe into the ground and rocks come unearthed ~ blocks away that then shift towards him, knocking back enemies immensely.
        }
    }
}