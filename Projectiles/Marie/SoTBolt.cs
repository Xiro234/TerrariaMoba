using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class SoTBolt : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 29;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
        }

        public override void AI() {
            for (int i = 0; i < 2; i++) {
                int sotDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GemSapphire, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 1.2f);
                Main.dust[sotDust].noGravity = true;
                Dust dust3 = Main.dust[sotDust];
                dust3.velocity *= 0.3f;
            }
            if (Projectile.ai[1] == 0f) {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            }
        }
    }
}