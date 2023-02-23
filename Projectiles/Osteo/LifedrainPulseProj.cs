﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Osteo;

namespace TerrariaMoba.Projectiles.Osteo {
    public class LifedrainPulseProj : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Lifedrain Pulse");
        }

        public int PulseLifetime { get; set; }

        public override void SetDefaults() {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.timeLeft = 1000;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            DrawOffsetX = -24;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hide = true;

            PulseLifetime = LifedrainPulse.PULSE_LIFETIME;
        }

        public override void AI() {
            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = PulseLifetime - 1;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch, 0f, 0f, 100, Color.DarkSlateBlue, 1.3f);
            Main.dust[dust].noGravity = true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            float num9 = 0f;
            Vector2 value = Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(-1.5707963705062866, default(Vector2)) * Projectile.scale;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(),
                Projectile.Center - value * 36f, Projectile.Center + value * 36f, 16f * Projectile.scale, ref num9);
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs,
            List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverPlayers, List<int> drawCacheProjsOverWiresUI) {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }

        public override Color? GetAlpha(Color lightColor) {
            return Color.Lerp(lightColor, Color.DarkSlateBlue, 0.5f);
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(PulseLifetime);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            PulseLifetime = reader.ReadInt32();
        }
    }
}