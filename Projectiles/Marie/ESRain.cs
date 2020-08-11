using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESRain : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 4;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.timeLeft = 300;
            projectile.scale = 1.1f;
            projectile.alpha = 60;
            projectile.magic = true;
            projectile.extraUpdates = 1;
        }
    }
}