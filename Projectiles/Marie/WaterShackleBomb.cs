using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class WaterShackleBomb : ModProjectile {
        public override void SetDefaults() {
            projectile.Name = "Water Shackle Bomb";
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.thrown = true;
            projectile.timeLeft = 300;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            if ((int)projectile.velocity.X != (int)oldVelocity.X && Math.Abs(oldVelocity.X) > 1f) {
                projectile.velocity.X = oldVelocity.X * -0.4f;
            }
            if ((int)projectile.velocity.Y != (int)oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f) {
                projectile.velocity.Y = oldVelocity.Y * -0.5f;
            }
            return false;
        }

        public override void AI() {
            if (projectile.owner == Main.myPlayer && projectile.timeLeft > 3) {
                for (int i = 0; i < 6; i++) {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 137, 0f, 0f, 0, default(Color));
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }

            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3) {
                projectile.tileCollide = false;
                projectile.alpha = 255;
                projectile.position = projectile.Center;
                projectile.width = 320;
                projectile.height = 320;
                projectile.Center = projectile.position;
                projectile.damage = 250;
                projectile.knockBack = 10f;
            }

            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 5f) {
                projectile.ai[0] = 10f;
                if ((int)projectile.velocity.Y == 0 && (int)projectile.velocity.X != 0) {
                    projectile.velocity.X = projectile.velocity.X * 0.95f;
                    if ((double)projectile.velocity.X > -0.01 && (double)projectile.velocity.X < 0.01)
                    {
                        projectile.velocity.X = 0f;
                        projectile.netUpdate = true;
                    }
                }
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }
            projectile.rotation += projectile.velocity.X * 0.1f;
        }

        public override void Kill(int timeLeft) {
            Vector2 bombPos = projectile.Center;
            Main.PlaySound(SoundID.Item14, projectile.Center);
            float radius = 10 * 16.0f;

            for (int a = 0; a < 360; a++) {
                int xPos = (int)(bombPos.X + radius * Math.Cos(TerrariaMobaUtils.Conv2Rad((double)a)));
                int yPos = (int)(bombPos.Y + radius * Math.Sin(TerrariaMobaUtils.Conv2Rad((double)a)));
                int dust = Dust.NewDust(new Vector2(xPos, yPos), 1, 1, 226, 0, 0, 0, default(Color));
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
            }
        }
    }
}