using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Jorm {
    public class HammerfallProj : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 72;
            projectile.height = 74;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 420;
        }

        public override void AI() {
            if (projectile.ai[0] > 0) {
                projectile.rotation = MathHelper.ToRadians(-90);
            }

            for (int d = 0; d < 8; d++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 269, 0, 0, 150);
            }
        }
    }
}