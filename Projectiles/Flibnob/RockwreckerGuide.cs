using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Flibnob;

namespace TerrariaMoba.Projectiles.Flibnob {
    public class RockwreckerGuide : ModProjectile {
        public int RockDamage { get; set; }
        public float RockKnockback { get; set; }
        public int GuideTimer { get; set; }
        
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Rockwrecker");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 112;
            Projectile.height = 32;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;

            RockDamage = Rockwrecker.ROCK_DAMAGE;
            RockKnockback = Rockwrecker.ROCK_KNOCKBACK;
            GuideTimer = Rockwrecker.GUIDE_LIFETIME;
        }

        public override void AI() {
            if (Projectile.ai[0] <= -1f) {
                Projectile.timeLeft = GuideTimer;
                Projectile.ai[0] += 1f;
            }

            Projectile.alpha = (int)Math.Abs(255 * Math.Sin(Math.PI * Projectile.ai[1] / 180.0));
            Projectile.ai[1] -= (float)360 / GuideTimer;
        }

        public override void Kill(int timeLeft) {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, 
                    ModContent.ProjectileType<RockwreckerProj>(), RockDamage, RockKnockback, Main.myPlayer);
            }
        }
    }
}