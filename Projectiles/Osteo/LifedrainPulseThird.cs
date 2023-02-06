using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class LifedrainPulseThird : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("LifedrainPulseThird");
        }

        public override string Texture => "TerrariaMoba/Projectiles/Osteo/LifedrainPulse";

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
            DrawOffsetX = -24;
            Projectile.hide = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.scale = 1.2f;
            Projectile.penetrate = -1;
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch, 0f, 0f, 100, Color.DarkRed, 2f);
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
            return Color.Lerp(lightColor, Color.BlueViolet, 0.5f);
        }
    }
}