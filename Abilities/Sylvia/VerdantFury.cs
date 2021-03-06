/*using Terraria;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Sylvia {
    public class VerdantFury : Ability {
        public VerdantFury(Player myPlayer) : base(myPlayer) {
            Name = "Verdant Fury";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaAbilityTwo");
        }

        public override void OnCast() {
            //player.AddBuff(BuffType<Buffs.VerdantFuryBuff>(), 60 * 4);
            if (Main.LocalPlayer == User) {
                User.GetModPlayer<MobaPlayer>().AddEffect(new Slow(360, User.whoAmI,0.3f));
            }
            cooldownTimer = 10 * 60;
        }
    }
}*/