using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Jorm {
    public class HammerfallProj : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 114;
            Projectile.height = 132;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 420;
        }

        public override void AI() {
            if (Projectile.timeLeft > 360) {
                Projectile.velocity = Vector2.Zero;
            } else {
                Projectile.velocity = new Vector2(0, Projectile.ai[0]);
            }

            for (int d = 0; d < 8; d++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sandnado, 0, 0, 150);
            }
        }
    }
}