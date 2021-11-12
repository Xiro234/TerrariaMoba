using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Abilities {
    public class TestRootAbility : Ability {
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }
        private const int ROOT_DURATION = 120;
        
        public TestRootAbility(Player player) : base(player, "TestRootAbility", 60, 0, AbilityType.Active) {
            
        }

        public override void OnCast() {
            StatusEffectManager.AddEffect(User, new FunRoot(ROOT_DURATION, true));
        }
    }
}