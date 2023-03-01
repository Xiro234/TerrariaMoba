using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrariaMoba.Abilities.Sylvia;

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
            if (Projectile.timeLeft < SporeDuration - 8) {
                return true;
            } else {
                return false;
            }
        }
    }
}