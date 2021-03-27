using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class IronRush : Ability {
        public IronRush() : base("Iron Rush", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Nocturne dashes forward, gaining armor for ~s.
        }
    }
}