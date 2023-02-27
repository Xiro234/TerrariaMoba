using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Sylvia;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class RoseThorn : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Rose Thorn");
        }
        
        public int ThornLifetime { get; set; }

        public override void SetDefaults() {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.timeLeft = 1000;
            Projectile.penetrate = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;

            ThornLifetime = WitheredRose.THORN_MAX_LIFETIME;
        }

        public override void AI() {
            int targetId = (int)Projectile.ai[0];

            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = ThornLifetime;
            }

            if (Projectile.localAI[0] == 0f) {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 1f;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Vector2 targetLoc = Vector2.Zero;
            bool targetFound = false;

            if (Main.player[targetId].active) {
                targetLoc = Main.player[targetId].Center - Projectile.Center;
                targetFound = true;
            } else {
                Kill(Projectile.timeLeft);
            }

            if (targetFound) {
                AdjustMagnitude(ref targetLoc);
                Projectile.velocity = (10 * Projectile.velocity + targetLoc) / 11f;
                AdjustMagnitude(ref Projectile.velocity);
            }
        }

        private void AdjustMagnitude(ref Vector2 vel) {
            float magnitude = (float)Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);
            if (magnitude > 6f) {
                vel *= 6f / magnitude;
            }
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 10; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WoodFurniture, 0f, 0f, 255, default, 1f);
            }
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(ThornLifetime);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            ThornLifetime = reader.ReadInt32();
        }
    }
}