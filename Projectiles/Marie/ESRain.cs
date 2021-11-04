using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESRain : ModProjectile {
        public override void SetDefaults() {
            Projectile.Name = "Storm Rain";
            Projectile.width = 4;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.scale = 1.1f;
            Projectile.alpha = 60;
            Projectile.extraUpdates = 1;
        }
    }
}