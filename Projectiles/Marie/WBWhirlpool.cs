using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Marie;

namespace TerrariaMoba.Projectiles.Marie {
    public class WBWhirlpool : ModProjectile {

        public int PoolDuration { get; set; }

        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults() {
            Projectile.width = 162;
            Projectile.height = 42;
            Projectile.timeLeft = 1000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        
        public override void AI() {
            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = PoolDuration;
            }

            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
        }
    }
}