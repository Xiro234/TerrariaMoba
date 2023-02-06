using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class TidecallerProj : ModProjectile {

        public override void SetDefaults() {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI() {
            int x = (int)Projectile.Bottom.X;
            int y = (int)Projectile.Bottom.Y;
            if (TerrariaMobaUtils.TileIsSolidOrPlatform(x / 16, y / 16)) {
                Projectile.width += 120;
                Projectile.height += 25;
                Projectile.position.X -= 60;
                Projectile.position.Y -= 25;
                Projectile.Kill();
            }

            Projectile.ai[0] += 1f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (Projectile.ai[0] < 15) {
                Projectile.alpha = (int)((255 / 15) * (15 - Projectile.ai[0]));
            } else {
                Projectile.alpha = 0;
            }

            Vector2 direction = Vector2.Normalize(Projectile.velocity);

            for (int i = 0; i < 3; i++) {
                float val = (float)Math.Sin(6 * MathHelper.ToRadians(Projectile.ai[0]) + (i / 2));
                Vector2 position1 = Projectile.Center + (new Vector2(-direction.Y, direction.X) * (val * 15));
                Vector2 position2 = Projectile.Center + (new Vector2(direction.Y, -direction.X) * (val * 15));

                Dust.NewDustPerfect(position1, DustID.WhiteTorch, Vector2.Zero, Projectile.alpha, Color.CornflowerBlue, 1.4f);
                Dust.NewDustPerfect(position2, DustID.WhiteTorch, Vector2.Zero, Projectile.alpha, Color.CornflowerBlue, 1.4f);
            }
        }

        public override void Kill(int timeLeft) {
            for (int i = 0; i < 40; i++) {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WhiteTorch, 0f, 0f, Projectile.alpha, Color.CornflowerBlue);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(50, 100) * 0.033f;
                Main.dust[dust].velocity *= 0.33f;
            }
        }
    }
}
