using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class WBWhirlpool : ModProjectile {

        public int PoolDuration { get; set; }

        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults() {
            Projectile.width = 162;
            Projectile.height = 42;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
        }
        
        public override void AI() {
            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
        }
    }
}