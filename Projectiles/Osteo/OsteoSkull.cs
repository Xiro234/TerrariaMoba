using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class OsteoSkull : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("OsteoSkull");
        }

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 24;
            projectile.height = 30;
            projectile.aiStyle = 0;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
        }

        public override void AI() {
            projectile.spriteDirection = projectile.direction;
            if (projectile.direction < 0) {
                projectile.rotation = (float)Math.Atan2((double)(0f - projectile.velocity.Y), (double)(0f - projectile.velocity.X));
            }
            else {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
            }

            for (int i = 0; i < 5; i++) {
                var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 0,
                    Color.SteelBlue, 1f);

                dust.noGravity = true;
                dust.noLight = true;
            }
        }

        public override void Kill(int timeLeft) {
            for (int i = 0; i < 5; i++) {
                var dust1 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 0,
                    Color.SteelBlue, 1f);

                dust1.noGravity = true;
                dust1.noLight = true;
                
                var dust2 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 84, 0f, 0f, 0,
                    Color.Gray, 1.2f);
                
                dust2.noGravity = true;
                dust2.noLight = true;
            }

            Main.PlaySound(0, projectile.position);
        }
    }
}