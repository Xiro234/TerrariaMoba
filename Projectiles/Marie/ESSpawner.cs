using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESSpawner : ModProjectile {
        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults() {
            Projectile.Name = "Storm Spawner";
            Projectile.netImportant = true;
            Projectile.width = 28; 
            Projectile.height = 28; 
            Projectile.timeLeft = 100;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
        }

        public override void AI() {
            if (Projectile.alpha > 0) {
                Projectile.alpha -= 8;
            }

            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
        }

        public override void Kill(int timeLeft) {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                Projectile proj = Projectile.NewProjectileDirect(Projectile.Center, Vector2.Zero, 
                    ModContent.ProjectileType<ESStormCloud"), Projectile.damage, Projectile.knockBack, Projectile.whoAmI);
                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);
            }
        }

        public override bool CanDamage() {
            return false;
        }
    }
}