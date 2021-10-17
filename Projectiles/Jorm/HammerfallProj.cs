using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Jorm {
    public class HammerfallProj : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 72;
            Projectile.height = 74;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 420;
        }

        public override void AI() {
            if (Projectile.ai[1] > 0) {
                Projectile.rotation = MathHelper.ToRadians(-90);
            }

            for (int d = 0; d < 8; d++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 269, 0, 0, 150);
            }
        }
    }
}