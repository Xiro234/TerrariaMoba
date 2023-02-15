using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class OsteoSkull : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("OsteoSkull");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 24;
            Projectile.height = 30;
            Projectile.aiStyle = 0;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
        }

        public override void AI() {
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.direction < 0) {
                Projectile.rotation = (float)Math.Atan2((double)(0f - Projectile.velocity.Y), (double)(0f - Projectile.velocity.X));
            }
            else {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            }

            for (int i = 0; i < 5; i++) {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror, 0f, 0f, 0,
                    Color.SteelBlue, 1f);

                dust.noGravity = true;
                dust.noLight = true;
            }
        }

        public override void Kill(int timeLeft) {
            for (int i = 0; i < 5; i++) {
                var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror, 0f, 0f, 0,
                    Color.SteelBlue, 1f);

                dust1.noGravity = true;
                dust1.noLight = true;
                
                var dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Platinum, 0f, 0f, 0,
                    Color.Gray, 1.2f);
                
                dust2.noGravity = true;
                dust2.noLight = true;
            }

            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}