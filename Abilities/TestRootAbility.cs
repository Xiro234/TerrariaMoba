using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Abilities {
    public class TestRootAbility : Ability {
        public override Texture2D Icon { get { return TerrariaMoba.Instance.GetTexture("Textures/Blank");} }
        private const int ROOT_DURATION = 120;
        
        public TestRootAbility() : base("TestRootAbility", 60, 0, AbilityType.Active) {
            
        }

        public override void OnCast() {
            StatusEffectManager.AddEffect(User, new FunRoot(ROOT_DURATION, true));
        }
    }
}