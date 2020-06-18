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
            projectile.timeLeft = 270;
            projectile.alpha = 255;
            projectile.friendly = true; 
            projectile.hostile = false; 
            projectile.tileCollide = false;
            projectile.ignoreWater = true; 
            projectile.magic = true; 
        }

        public override void AI() {
            projectile.ai[0] += 1f;

            if (projectile.alpha > 0)
                projectile.alpha -= 5;

            if (++projectile.frameCounter >= 5) {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }

            if ((int)projectile.ai[0] == 90 && projectile.owner == Main.myPlayer) {
                projectile.netUpdate = true;
                projectile.velocity = new Vector2(0, 0);
            }

            if ((int) projectile.ai[0] % 45 == 0 && projectile.owner == Main.myPlayer)
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 
                    MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), 2f, 
                    TerrariaMoba.Instance.ProjectileType("MaelstromBeam"), 30, 0f, projectile.owner)];
                Main.PlaySound(SoundID.Item122, projectile.Center);
            }
        }

        public override bool CanDamage() {
            return false;
        }
    }
}