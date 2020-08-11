using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class SoTBolt : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 29;
            projectile.alpha = 255;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.friendly = true;
        }

        public override void AI() {
            for (int i = 0; i < 2; i++) {
                int sotDust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 88, projectile.velocity.X, projectile.velocity.Y, 50, default(Color), 1.2f);
                Main.dust[sotDust].noGravity = true;
                Dust dust3 = Main.dust[sotDust];
                dust3.velocity *= 0.3f;
            }
            if (projectile.ai[1] == 0f) {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item8, projectile.position);
            }
        }
    }
}