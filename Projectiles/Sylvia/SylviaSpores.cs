using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrariaMoba.Abilities.Sylvia;
using Terraria.GameContent;
using System;
using Terraria.ID;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaSpores : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Plantera's Spore");
            Main.projFrames[Projectile.type] = 5;
        }

        public int SporeDuration { get; set; }
        
        public override void SetDefaults() {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 1000;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            SporeDuration = PlanterasLastWill.SPORE_DURATION;
        }

        public override void AI() {
            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }

            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = SporeDuration;
            }

            if (Projectile.ai[0] >= 60) {
                Projectile.ai[0] = 0f;
                const float magnitude = 8f;
                for (int i = 0; i < 16; i++) {
                    Vector2 offset = new();
                    float angleDegrees = (360 / 16) * i;
                    float angleRadians = MathHelper.ToRadians(angleDegrees);
                    offset.X = (float)(Math.Sin(angleRadians) * magnitude);
                    offset.Y = (float)(Math.Cos(angleRadians) * magnitude);
                    Dust dust = Main.dust[Dust.NewDust(Projectile.Center, Projectile.width / 2, Projectile.height / 2,
                        DustID.Enchanted_Gold, offset.X, offset.Y, 190, Color.LightGreen, 1.2f)];
                    dust.noGravity = true;
                    dust.velocity *= 0.95f;
                }
            }
            Projectile.ai[0] += 1f;

            if (Projectile.timeLeft < 60) {
                Projectile.alpha += (255 / 60);
            }

            if (Projectile.velocity.Length() > 0) {
                Vector2 direction = Vector2.Normalize(Projectile.velocity);
                direction *= 0.1f;

                if (Projectile.velocity.Length() < direction.Length()) {
                    Projectile.velocity = Vector2.Zero;
                } else {
                    Projectile.velocity -= direction;
                }
            }
        }

        public override bool CanHitPvp(Player target) {
            if (Projectile.timeLeft < SporeDuration - 10) {
                return true;
            } else {
                return false;
            }
        }
    }
}