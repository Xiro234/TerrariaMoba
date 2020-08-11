using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESSpawner : ModProjectile {
        public override void SetStaticDefaults() {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults() {
            projectile.Name = "Storm Spawner";
            projectile.netImportant = true;
            projectile.width = 28; 
            projectile.height = 28; 
            projectile.timeLeft = 100;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
        }

        public override void AI() {
            if (projectile.alpha > 0) {
                projectile.alpha -= 8;
            }

            if (++projectile.frameCounter >= 5) {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }
        }

        public override void Kill(int timeLeft) {
            Player player = Main.player[projectile.owner];
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, 
                    TerrariaMoba.Instance.ProjectileType("ESStormCloud"), projectile.damage, projectile.knockBack, player.whoAmI, 0f, 0f);
                Main.PlaySound(SoundID.Item74, projectile.Center);
            }
        }

        public override bool CanDamage() {
            return false;
        }
    }
}