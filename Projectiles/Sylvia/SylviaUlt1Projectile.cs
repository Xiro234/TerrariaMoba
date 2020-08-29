using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt1Projectile : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt1");
        }
        
        public override void SetDefaults() {
            projectile.height = 20;
            projectile.width = 20;
            projectile.friendly = true;
            projectile.aiStyle = 0;
            projectile.tileCollide = true;
        }

        public override void AI() {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            for (int i = 0; i < 5; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
        }
        
        public override void Kill(int timeLeft) {
            Main.PlaySound(0, projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 3, 0,
                    0, 0, default(Color), 1f);
            }
        }
    }
}