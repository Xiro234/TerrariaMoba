using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class WeightedStrike : Ability {
        public WeightedStrike() : base("Weighted Strike", 180, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void WhileActive() {
            //TODO - Every 20s Nocturnes next basic attack deals 4% of the targets max health as bonus physical damage, cd reduced on AA hit by 2s.
        }
    }
}