using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaSpores : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaSpores");
            Main.projFrames[Projectile.type] = 5;
        }
        
        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 0;
        }

        public override void AI() {
            Projectile.ai[0] += 1f;
            
            if (Projectile.velocity.Length() > 0) {
                Vector2 direction = Vector2.Normalize(Projectile.velocity);
                direction *= 0.1f;

                if (Projectile.velocity.Length() < direction.Length()) {
                    Projectile.velocity = Vector2.Zero;
                }
                else {
                    Projectile.velocity -= direction;
                }
            }


            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }

            if (Projectile.ai[0] > 300) {
                Projectile.alpha += (255 / 60);
            }
            
            if (Projectile.ai[0] > 360) {
                Projectile.Kill();
            }
        }
    }
}