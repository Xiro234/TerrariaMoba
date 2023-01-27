using System;
using System.IO;
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

        public override bool? CanDamage() {
            return false;
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
                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), 
                    Projectile.Center, Vector2.Zero, ModContent.ProjectileType<RockwreckerProj>(), 
                    1, RockKnockback, Main.myPlayer);
                TerrariaMobaUtils.SetProjectileDamage(proj, PhysicalDamage: RockDamage);
            }
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(RockDamage);
            writer.Write(RockKnockback);
            writer.Write(GuideTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            RockDamage = reader.ReadInt32();
            RockKnockback = reader.ReadSingle();
            GuideTimer = reader.ReadInt32();
        }
    }
}