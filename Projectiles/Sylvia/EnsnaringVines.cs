using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using Microsoft.Xna.Framework;
using TerrariaMoba.Packets;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class EnsnaringVines : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("EnsnaringVines");
        }

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 34;
            projectile.height = 26;
            projectile.tileCollide = false;
        }

        public override void AI() {
            if (projectile.ai[0] == 0) {
                Main.PlaySound(6, projectile.position);
                for (int i = 0; i < 20; i++) {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
                        0, 150, Color.LightGreen, 0.7f);
                }
            }
            projectile.ai[0] += 1f;
            
            /*
            var player = Main.player[projectile.owner].GetModPlayer<MobaPlayer>();
            //Venus Flytrap
            if (player.MyCharacter.talentArray[2, 2]) {
                if (projectile.ai[0] >= 540) {
                    projectile.Kill();
                }
            }
            else {
            */
                if (projectile.ai[0] >= 330) {
                    projectile.Kill();
                }
        }

        public override void Kill(int timeLeft) {
            Main.PlaySound(6, projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            var player = Main.player[projectile.owner].GetModPlayer<MobaPlayer>();

            target.AddBuff(BuffType<Buffs.EnsnaringVines>(), 90, false);

            //Quashing Shrubbery
            if (player.MyCharacter.talentArray[1, 2]) {
                target.AddBuff(BuffType<Buffs.Silenced>(), 90, false);
            }

            if (player.MyCharacter.talentArray[1, 1]) {
                SyncWeakenedPacket.Write(target.whoAmI, 90, 0.12f, "Ensnaring Vines");
            }
        }
    }
}