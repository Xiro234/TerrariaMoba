using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class Riptide : Ability {
        public Riptide() : base("Riptide", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Old Man lets it rip beyblade style and yeets a Razorblade Typhoon projectile that attempts to home and rip enemies to pieces if the initial projectile hits an enemy.
        }
    }
}