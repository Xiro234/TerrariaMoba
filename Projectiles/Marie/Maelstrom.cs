using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class Maelstrom : ModProjectile {
        public override void SetStaticDefaults() {
            Main.projFrames[projectile.type] = 4;   
        }

        public override void SetDefaults() {
            projectile.Name = "Maelstrom";
            projectile.width = 100; 
            projectile.height = 100; 
            projectile.timeLeft = 280;
            projectile.alpha = 255;
            projectile.friendly = true; 
            projectile.hostile = false; 
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI() {
            Player player = Main.player[projectile.owner];
            projectile.ai[0] += 1f;

            if (projectile.alpha > 0) {
                projectile.alpha -= 5;
            }

            if (++projectile.frameCounter >= 5) {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }

            if ((int)projectile.ai[0] == 90) {
                projectile.netUpdate = true;
                projectile.velocity = new Vector2(0, 0);
            }

            if ((int) projectile.ai[0] >= 90) {
                if ((int) projectile.ai[0] % 45 == 0) {
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y,
                            MathHelper.Lerp(-1f, 1f, (float) Main.rand.NextDouble()), 3f,
                            TerrariaMoba.Instance.ProjectileType("MaelstromBeam"), 30, 0f, player.whoAmI);
                    }
                    Main.PlaySound(SoundID.Item122, projectile.Center);
                }
            }
        }
    }
}