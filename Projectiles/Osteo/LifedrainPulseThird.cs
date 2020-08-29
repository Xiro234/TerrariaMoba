using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class LifedrainPulseThird : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("LifedrainPulseThird");
        }

        public override string Texture => "TerrariaMoba/Projectiles/Osteo/LifedrainPulse";

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 0;
            drawOffsetX = -24;
            projectile.hide = true;
            projectile.tileCollide = true;
            projectile.alpha = 255;
            projectile.scale = 1.2f;
            projectile.penetrate = -1;
        }

        public override void AI() {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            int dust = Dust.NewDust(projectile.Center, 0, 0, 60, 0f, 0f, 100, Color.DarkRed, 2f);
            Main.dust[dust].noGravity = true;
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
            return Color.Lerp(lightColor, Color.BlueViolet, 0.5f);
        }
    }
}