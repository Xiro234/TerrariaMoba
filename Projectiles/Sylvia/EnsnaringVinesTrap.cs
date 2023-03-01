using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using System;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class EnsnaringVinesTrap : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ensnaring Vine Trap");
            Main.projFrames[Projectile.type] = 2;
        }
        
        public int TrapDuration { get; set; }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 48;
            Projectile.height = 32;
            Projectile.timeLeft = 1000;
            Projectile.tileCollide = false;

            TrapDuration = EnsnaringVines.TRAP_DURATION;
        }
        
        public override void AI() {
            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = TrapDuration;
            }

            if (++Projectile.frameCounter >= 12) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }

            if ((int)Projectile.ai[0] == 0) {
                SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
                const float distance = 60f;
                for (int i = 0; i < 20; i++) {
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * distance);
                    offset.Y += (float)(Math.Cos(angle) * distance);
                    Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset, 0, 0, DustID.Enchanted_Gold, 0, 0, 150, Color.LightGreen, 0.9f)];
                    dust.velocity += Vector2.Normalize(offset) * -5f;
                    dust.noGravity = true;
                }
                Projectile.ai[0] += 1f;
            }
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Grass, Projectile.position);

            const float magnitude = 10f;
            for (int i = 0; i < 20; i++) {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * magnitude);
                offset.Y += (float)(Math.Cos(angle) * magnitude);
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height / 2, 
                    DustID.Enchanted_Gold, offset.X, offset.Y, 150, Color.LightGreen, 0.9f)];
                dust.noGravity = true;
            }
        }
     }
}