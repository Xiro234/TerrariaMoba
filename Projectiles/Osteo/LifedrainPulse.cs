using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class LifedrainPulse : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("LifedrainPulse");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
            DrawOffsetX = -24;
            Projectile.hide = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch, 0f, 0f, 100, Color.DarkSlateBlue, 1.3f);
            Main.dust[dust].noGravity = true;
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            
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
    }
}