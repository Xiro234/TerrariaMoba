using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class MaelstromBeam : ModProjectile {
        
        public override void SetDefaults() {
            projectile.Name = "Maelstrom Beam";
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 4;
            projectile.timeLeft = 600;
            projectile.magic = true;
        }

        public override void AI() {
            for (int i = 0; i < 4; i++) {
                Vector2 projPos = projectile.position;
                projPos -= projectile.velocity * ((float)i * 0.25f);
                projectile.alpha = 255;
                int dust = Dust.NewDust(Vector2.Zero, 0, 0, 1, 0f, 0f, 0, default(Color), 1f);
                
                if (Main.rand.Next(1) == 0) {
                    int choice = Main.rand.Next(3);
                    if (choice == 0)
                        choice = 226;
                    else if (choice == 1)
                        choice = 34;
                    else
                        choice = 92;
                    dust = Dust.NewDust(projPos, 1, 1, choice, 0f, 0f, 0, default(Color), 1f);
                }
                
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = projPos;
                Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                Main.dust[dust].velocity *= 0.2f;
            }
        }

        public override bool CanDamage() {
            return true;
        }
    }
}