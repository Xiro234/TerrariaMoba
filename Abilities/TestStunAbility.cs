using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities {
    public class TestStunAbility : Ability {
        public override Texture2D Icon { get { return TerrariaMoba.Instance.GetTexture("Textures/Blank");} }
        private const int STUN_DURATION = 120;

        public TestStunAbility() : base("TestStunAbility", 60, 0, AbilityType.Active) {
            
        }
        
        public override void OnCast() {
            Main.NewText(User.whoAmI + " " + Main.LocalPlayer.whoAmI);
            StatusEffectManager.AddEffect(User, new FunStun(STUN_DURATION, true));
        }
    }
}