using Microsoft.Xna.Framework;
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
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == projectile.owner) {
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, 
                    TerrariaMoba.Instance.ProjectileType("ESStormCloud"), projectile.damage, projectile.knockBack, projectile.whoAmI);
                Main.PlaySound(SoundID.Item74, projectile.Center);
            }
        }

        public override bool CanDamage() {
            return false;
        }
    }
}