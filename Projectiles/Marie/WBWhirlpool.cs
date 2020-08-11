using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class WBWhirlpool : ModProjectile {
        public override void SetStaticDefaults() {
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults() {
            projectile.width = 162;
            projectile.height = 42;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 150;
        }
        
        public override void AI() {
            if (++projectile.frameCounter >= 5) {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }
        }
    }
}