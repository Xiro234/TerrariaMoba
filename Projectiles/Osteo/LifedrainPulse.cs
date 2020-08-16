using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class LifedrainPulse : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("LifedrainPulse");
        }

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 0;
            drawOffsetX = -24;
            projectile.hide = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 60;
            projectile.alpha = 255;
            projectile.penetrate = -1;
        }

        public override void AI() {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            int dust = Dust.NewDust(projectile.Center, 0, 0, 60, 0f, 0f, 100, Color.DarkSlateBlue, 1.3f);
            Main.dust[dust].noGravity = true;

            if (projectile.timeLeft > 30) {
                projectile.alpha = (int)((projectile.timeLeft - 30) * ((float)255 / 30));
                Main.dust[dust].alpha = (int)((projectile.timeLeft - 30) * ((float)255 / 30));
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            float num9 = 0f;
            Vector2 value = projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(-1.5707963705062866, default(Vector2)) * projectile.scale;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(),
                projectile.Center - value * 36f, projectile.Center + value * 36f, 16f * projectile.scale, ref num9);
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs,
            List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI) {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }

        public override Color? GetAlpha(Color lightColor) {
            return Color.Lerp(lightColor, Color.DarkSlateBlue, 0.5f);
        }
    }
}