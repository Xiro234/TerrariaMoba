using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Flibnob {
    public class RockwreckerProj : ModProjectile {
        public override void SetDefaults() {
            Projectile.Name = "Rockwrecker";
            Projectile.width = 112;
            Projectile.height = 32;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
        }
    }
}