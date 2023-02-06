using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class RoseThorn : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Rose Thorn");
        }
        
        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
        }

        public override void AI() {
            int targetId = (int)Projectile.ai[0];
            
            if (Projectile.localAI[0] == 0f) {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 1f;
            }
            
            Vector2 targetLoc = Vector2.Zero;
            float currentDist = 400f;
            bool targetFound = false;

            if (Main.player[targetId].active) {
                Vector2 newTargetLoc = Main.player[targetId].Center - Projectile.Center;
                float distToTarget = (float)Math.Sqrt(newTargetLoc.X * newTargetLoc.X + newTargetLoc.Y * newTargetLoc.Y);
                if (distToTarget < currentDist) {
                    targetLoc = newTargetLoc;
                    currentDist = distToTarget;
                    targetFound = true;
                }
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
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WoodFurniture, 0f, 0f, 255, Color.Red, 1f);
            }
        }
    }
}