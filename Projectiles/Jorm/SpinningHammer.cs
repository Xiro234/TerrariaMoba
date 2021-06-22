using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Jorm {
    public class SpinningHammer : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 38;
            projectile.height = 38;
            projectile.timeLeft = 400;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
    }
}