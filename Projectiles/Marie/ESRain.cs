using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESRain : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 4;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            Projectile.scale = 1.1f;
            Projectile.alpha = 60;
            Projectile.extraUpdates = 1;
        }
    }
}