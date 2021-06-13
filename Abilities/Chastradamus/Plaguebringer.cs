using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class Plaguebringer : Ability {
        public Plaguebringer() : base("Plaguebringer", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Chastradamus does something rather contradictory and gives everyone the bubonic plague.
        }
    }
}