using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESLightning : ModProjectile {
        public override void SetDefaults() {
            projectile.Name = "Maelstrom Beam";
            projectile.width = 14;
            projectile.height = 14;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 4;
            projectile.timeLeft = 600;
        }

        public override void AI() {
            for (int i = 0; i < 4; i++) {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 226);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(60, 100) * 0.013f;
                Main.dust[dust].velocity *= 0.2f;
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            target.AddBuff(BuffID.Electrified, 75, false);
        }
    }
}