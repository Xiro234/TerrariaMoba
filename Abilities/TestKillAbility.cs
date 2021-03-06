using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.Abilities {
    public sealed class TestKillAbility : Ability, IKill {
        public override Texture2D Icon { get { return TerrariaMoba.Instance.GetTexture("Textures/Blank");} }

        public void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
        }

        public TestKillAbility() : base("TestKillAbility", 60, 0, AbilityType.Active) {
            
        }

        public override void OnCast() {
            Main.NewText(User.whoAmI + " " + Main.LocalPlayer.whoAmI);
            User.KillMe(PlayerDeathReason.LegacyDefault(), 10, 0, false);
        }

        public override bool CanCastAbility() {
            return true;
        }
    }
}