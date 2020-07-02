using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaSpores : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaSpores");
            Main.projFrames[projectile.type] = 5;
        }
        
        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 0;
        }

        public override void AI() {
            projectile.ai[0] += 1f;
            
            if (projectile.velocity.Length() > 0) {
                Vector2 direction = Vector2.Normalize(projectile.velocity);
                direction *= 0.1f;

                if (projectile.velocity.Length() < direction.Length()) {
                    projectile.velocity = Vector2.Zero;
                }
                else {
                    projectile.velocity -= direction;
                }
            }


            if (++projectile.frameCounter >= 5) {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }

            if (projectile.ai[0] > 300) {
                projectile.alpha += (255 / 60);
            }
            
            if (projectile.ai[0] > 360) {
                projectile.Kill();
            }
        }
    }
}