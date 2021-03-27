using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class DanceOfTheGoldenhammer : Ability {
        public DanceOfTheGoldenhammer() : base("Dance of the Goldenhammer", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - 4 hammers spin around him, damage and daze on hit (they break on collide).
        }
    }
}