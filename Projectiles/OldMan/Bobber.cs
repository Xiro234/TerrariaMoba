using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.OldMan {
    public class Bobber : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("OldManBobber");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            DrawOriginOffsetY = -8;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 10; i++) {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                    Projectile.height, 7, 0f, 0f, 0, Color.Red, 1f);
            }
        }
        
        public override void ModifyFishingLine(ref Vector2 lineOriginOffset, ref Color lineColor) {
            lineOriginOffset = new Vector2(47, -31);
        }

        public override void AI() {
	        Player player = Main.player[Projectile.owner];
	        if (Projectile.ai[0] >= 1f)
			{
				if (Projectile.localAI[0] < 100f)
				{
					Projectile.localAI[0] += 1f;
				}
				Projectile.tileCollide = false;
				int num = 10;
				Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num2 = player.position.X + (float)(player.width / 2) - vector.X;
				float num3 = player.position.Y + (float)(player.height / 2) - vector.Y;
				float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
				if (num4 > 3000f)
				{
					Projectile.Kill();
				}
				num4 = 15.9f / num4;
				num2 *= num4;
				num3 *= num4;
				Projectile.velocity.X = (Projectile.velocity.X * (float)(num - 1) + num2) / (float)num;
				Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num - 1) + num3) / (float)num;
				Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
				if (Main.myPlayer == Projectile.owner && Projectile.Hitbox.Intersects(player.Hitbox))
				{
					Projectile.Kill();
				}
				return;
			}
			bool flag2 = false;
			Vector2 vector2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num5 = player.position.X + (float)(player.width / 2) - vector2.X;
			float num6 = player.position.Y + (float)(player.height / 2) - vector2.Y;
			Projectile.rotation = (float)Math.Atan2(num6, num5) + 1.57f;
			if ((float)Math.Sqrt(num5 * num5 + num6 * num6) > 900f)
			{
				Projectile.ai[0] = 1f;
			}

	        if (Projectile.velocity.Y == 0f) {
		        Projectile.velocity.X *= 0.95f;
	        }
	        Projectile.velocity.X *= 0.98f;
	        Projectile.velocity.Y += 0.2f;
	        if (Projectile.velocity.Y > 15.9f) {
		        Projectile.velocity.Y = 15.9f;
	        }
	        if (Projectile.ai[1] != 0f)
			{
				flag2 = true;
			}
	        player.heldProj = Projectile.whoAmI;
	        player.SetDummyItemTime(2);
        }
    }
}