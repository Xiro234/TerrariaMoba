using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Nocturne {
    public class UmbralBlade : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Umbral Blade");
        }
        
        public override void SetDefaults() {
            projectile.height = 136;
            projectile.width = 38;
            projectile.friendly = true;
            projectile.aiStyle = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 240;
            projectile.penetrate = -1;
        }

        public override void AI() {
            for (int i = 0; i < 5; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 36, 0,
                    0, 100);
            }
        }
        
        public override void Kill(int timeLeft) {
            Main.PlaySound(0, projectile.position);
            for (int i = 0; i < 40; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 36);
            }
        }
    }
}