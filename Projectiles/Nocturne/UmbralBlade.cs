using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Nocturne {
    public class UmbralBlade : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Umbral Blade");
        }
        
        public override void SetDefaults() {
            Projectile.height = 136;
            Projectile.width = 38;
            Projectile.friendly = true;
            Projectile.aiStyle = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 240;
            Projectile.penetrate = -1;
        }

        public override void AI() {
            for (int i = 0; i < 5; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 36, 0,
                    0, 100);
            }
        }
        
        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 40; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 36);
            }
        }
    }
}