using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Projectiles {
    public class EnrapturingVines : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("EnrapturingVines");
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

            if (projectile.ai[0] >= 300) {
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
            target.AddBuff(BuffType<Buffs.EnrapturingVines>(), 240, false);
        }
    }
}