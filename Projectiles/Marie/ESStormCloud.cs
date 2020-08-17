using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            Player player = Main.player[projectile.owner];
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
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                    int rainX = (int) (projectile.position.X + 14f + Main.rand.Next(projectile.width - 18));
                    int rainY = (int) projectile.position.Y + projectile.height;
                    Projectile.NewProjectile(rainX, rainY, 0f, 4.25f,
                        TerrariaMoba.Instance.ProjectileType("ESRain"), projectile.damage, 0f, player.whoAmI, 0f, 0f);
                }
            }

            if (projectile.ai[1] >= 45f) {
                projectile.ai[1] = 0f;
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                    int rainX = (int) (projectile.position.X + 14f + Main.rand.Next(projectile.width - 18));
                    int rainY = (int) (projectile.position.Y + projectile.height - 20f);
                    Projectile.NewProjectile(rainX, rainY, 0f, 3.5f,
                        TerrariaMoba.Instance.ProjectileType("ESLightning"), projectile.damage * 3, 0f, player.whoAmI, 0f, 0f);
                }
            }
        }
    }
}