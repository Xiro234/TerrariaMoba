using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESStormCloud : ModProjectile {
        public override void SetStaticDefaults() {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults() {
            projectile.Name = "ES Storm Cloud";
            projectile.width = 366; 
            projectile.height = 104; 
            projectile.timeLeft = 380;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
        }

        public override void AI() {
            if (projectile.timeLeft > 20) {
                projectile.ai[0] += 1f;
                projectile.ai[1] += 1f;
            }

            if (++projectile.frameCounter >= 5) {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }
            
            if (projectile.ai[0] >= 4f) {
                projectile.ai[0] = 0f;
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == projectile.owner) {
                    int rainX = (int) (projectile.position.X + 14f + Main.rand.Next(projectile.width - 18));
                    int rainY = (int) projectile.position.Y + projectile.height;
                    Vector2 pos = new Vector2(rainX, rainY);
                    Vector2 vel = new Vector2(0f, 4.25f);
                    Projectile.NewProjectile(pos, vel, TerrariaMoba.Instance.ProjectileType("ESRain"), 
                        0, 0f, projectile.whoAmI);
                }
            }

            if (projectile.ai[1] >= 45f) {
                projectile.ai[1] = 0f;
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == projectile.owner) {
                    int lightX = (int) (projectile.position.X + 14f + Main.rand.Next(projectile.width - 18));
                    int lightY = (int) (projectile.position.Y + projectile.height - 20f);
                    Vector2 pos = new Vector2(lightX, lightY);
                    Vector2 vel = new Vector2(0f, 3.5f);
                    Projectile.NewProjectile(pos, vel, TerrariaMoba.Instance.ProjectileType("ESLightning"), 
                        0, 0f, projectile.whoAmI);
                }
            }
        }
    }
}