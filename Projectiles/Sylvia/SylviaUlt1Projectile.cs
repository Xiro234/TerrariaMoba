using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt1Projectile : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt1");
        }
        
        public override void SetDefaults() {
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.tileCollide = true;
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            for (int i = 0; i < 5; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
        }
        
        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 3, 0,
                    0, 0, default(Color), 1f);
            }
        }
    }
}