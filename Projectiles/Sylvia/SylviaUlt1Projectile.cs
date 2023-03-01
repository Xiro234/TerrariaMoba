using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using System;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt1Projectile : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Chlorophyte Javelin");
        }
        
        public override void SetDefaults() {
            Projectile.height = 100;
            Projectile.width = 30;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Vector2 direction = Vector2.Normalize(Projectile.velocity);
            for (int i = 0; i < 3; i++) {
                float val = (float)Math.Sin(6 * MathHelper.ToRadians(Projectile.ai[0]) + (i / 2));
                Vector2 position1 = Projectile.Center + (new Vector2(-direction.Y, direction.X) * (val * 14));
                Vector2 position2 = Projectile.Center + (new Vector2(direction.Y, -direction.X) * (val * 14));

                Dust.NewDustPerfect(position1, DustID.Enchanted_Gold, Vector2.Zero, Projectile.alpha, Color.LightGreen, 1.2f);
                Dust.NewDustPerfect(position2, DustID.Enchanted_Gold, Vector2.Zero, Projectile.alpha, Color.LightGreen, 1.2f);
            }
            Projectile.ai[0]++;
        }
        
        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GrassBlades, 0, 0, 0, default, 1f);
            }
        }
    }
}