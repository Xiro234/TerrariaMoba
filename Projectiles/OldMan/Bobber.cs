using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.OldMan {
    public class Bobber : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("OldManBobber");
        }
        
        private enum AIState {
	        Casting,
	        Retract
        }
        
        private AIState CurrentAIState {
	        get => (AIState)Projectile.ai[0];
	        set => Projectile.ai[0] = (float)value;
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
	        switch (CurrentAIState) {
		        case AIState.Retract: {
			        if (Projectile.localAI[0] < 100f) {
				        Projectile.localAI[0] += 1f;
			        }
		        
			        Projectile.tileCollide = false;
			        int num = 10;
			        Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f,
				        Projectile.position.Y + (float)Projectile.height * 0.5f);
			        float num2 = player.position.X + (float)(player.width / 2) - vector.X;
			        float num3 = player.position.Y + (float)(player.height / 2) - vector.Y;
			        float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
			        if (num4 > 3000f) {
				        Projectile.Kill();
			        }

			        num4 = 15.9f / num4;
			        num2 *= num4;
			        num3 *= num4;
			        Projectile.velocity.X = (Projectile.velocity.X * (float)(num - 1) + num2) / (float)num;
			        Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num - 1) + num3) / (float)num;
			        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
			        if (Main.myPlayer == Projectile.owner && Projectile.Hitbox.Intersects(player.Hitbox)) {
				        Projectile.Kill();
			        }

			        return;
		        }

		        case AIState.Casting: {
			        Vector2 vector2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f,
				        Projectile.position.Y + (float)Projectile.height * 0.5f);
			        float num5 = player.position.X + (float)(player.width / 2) - vector2.X;
			        float num6 = player.position.Y + (float)(player.height / 2) - vector2.Y;
			        Projectile.rotation = (float)Math.Atan2(num6, num5) + 1.57f;
			        if ((float)Math.Sqrt(num5 * num5 + num6 * num6) > 900f) { //pythagoran theorem, num5, num6 are 2 sides of a triangle
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

			        player.heldProj = Projectile.whoAmI;

			        if (player.itemTime <= 2) {
				        player.SetDummyItemTime(2);
			        }

			        break;
		        }
	        }
	        
	        
	        /*if (Projectile.ai[0] >= 1f) {
		        if (Projectile.localAI[0] < 100f) {
			        Projectile.localAI[0] += 1f;
		        }
		        
		        Projectile.tileCollide = false;
		        int num = 10;
		        Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f,
			        Projectile.position.Y + (float)Projectile.height * 0.5f);
		        float num2 = player.position.X + (float)(player.width / 2) - vector.X;
		        float num3 = player.position.Y + (float)(player.height / 2) - vector.Y;
		        float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
		        if (num4 > 3000f) {
			        Projectile.Kill();
		        }

		        num4 = 15.9f / num4;
		        num2 *= num4;
		        num3 *= num4;
		        Projectile.velocity.X = (Projectile.velocity.X * (float)(num - 1) + num2) / (float)num;
		        Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num - 1) + num3) / (float)num;
		        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		        if (Main.myPlayer == Projectile.owner && Projectile.Hitbox.Intersects(player.Hitbox)) {
			        Projectile.Kill();
		        }

		        return;
	        }

	        bool flag2 = false;
	        Vector2 vector2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f,
		        Projectile.position.Y + (float)Projectile.height * 0.5f);
	        float num5 = player.position.X + (float)(player.width / 2) - vector2.X;
	        float num6 = player.position.Y + (float)(player.height / 2) - vector2.Y;
	        Projectile.rotation = (float)Math.Atan2(num6, num5) + 1.57f;
	        if ((float)Math.Sqrt(num5 * num5 + num6 * num6) > 900f) { //pythagoran theorem, num5, num6 are 2 sides of a triangle
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

	        if (Projectile.ai[1] != 0f) {
		        flag2 = true;
	        }

	        player.heldProj = Projectile.whoAmI;

	        if (player.itemTime <= 2) {
		        player.SetDummyItemTime(2);
	        }*/
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity) {
	        if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
		        Projectile.velocity.X = -oldVelocity.X * 0.5f;
	        }
	        
	        if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
		        Projectile.velocity.Y = -oldVelocity.Y * 0.5f;
	        }
	        
	        return false;
        }

        public override bool PreDrawExtras() {
	        if (Projectile.ai[0] >= 0) {
		        Player player = Main.player[Projectile.owner];
		        if (!player.active) return true;
		        float pPosX = player.Center.X + 47 * player.direction;
		        float pPosY = player.Center.Y - 38;
		        float gravDir = player.gravDir;
		        if (gravDir == -1f) pPosY -= 12f;

		        Vector2 value = new Vector2(pPosX, pPosY);
		        value = player.RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
		        float projPosX = Projectile.position.X + Projectile.width * 0.5f - value.X;
		        float projPosY = Projectile.position.Y + Projectile.height * 0.5f - value.Y;
		        float rotation2 = (float)Math.Atan2(projPosY, projPosX) - 1.57f;
		        bool flag2 = true;

		        if (projPosX == 0f && projPosY == 0f) {
			        flag2 = false;
		        }
		        else {
			        float projPosXY = (float)Math.Sqrt((projPosX * projPosX + projPosY * projPosY));
			        projPosXY = 12f / projPosXY;
			        projPosX *= projPosXY;
			        projPosY *= projPosXY;
			        value.X -= projPosX;
			        value.Y -= projPosY;
			        projPosX = Projectile.position.X + Projectile.width * 0.5f - value.X;
			        projPosY = Projectile.position.Y + Projectile.height * 0.5f - value.Y;
		        }

		        while (flag2) {
			        float num = 12f;
			        float num2 = (float)Math.Sqrt((projPosX * projPosX + projPosY * projPosY));
			        float num3 = num2;

			        if (float.IsNaN(num2) || float.IsNaN(num3)) {
				        flag2 = false;
			        }
			        else {
				        if (num2 < 20f) {
					        num = num2 - 8f;
					        flag2 = false;
				        }

				        num2 = 12f / num2;
				        projPosX *= num2;
				        projPosY *= num2;
				        value.X += projPosX;
				        value.Y += projPosY;
				        projPosX = Projectile.position.X + Projectile.width * 0.5f - value.X;
				        projPosY = Projectile.position.Y + Projectile.height * 0.1f - value.Y;
				        if (num3 > 12f) {
					        float num4 = 0.3f;
					        float num5 = Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y);
					        if (num5 > 16f) {
						        num5 = 16f;
					        }

					        num5 = 1f - num5 / 16f;
					        num4 *= num5;
					        num5 = num3 / 80f;
					        if (num5 > 1f) {
						        num5 = 1f;
					        }

					        num4 *= num5;
					        if (num4 < 0f) {
						        num4 = 0f;
					        }

					        num5 = 1f - Projectile.localAI[0] / 100f;
					        num4 *= num5;
					        if (projPosY > 0f) {
						        projPosY *= 1f + num4;
						        projPosX *= 1f - num4;
					        }
					        else {
						        num5 = Math.Abs(Projectile.velocity.X) / 3f;
						        if (num5 > 1f) {
							        num5 = 1f;
						        }

						        num5 -= 0.5f;
						        num4 *= num5;
						        if (num4 > 0f) {
							        num4 *= 2f;
						        }

						        projPosY *= 1f + num4;
						        projPosX *= 1f - num4;
					        }
				        }

				        rotation2 = (float)Math.Atan2(projPosY, projPosX) - 1.57f;

				        Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f),
					        Color.White);
				        Main.spriteBatch.Draw(TextureAssets.FishingLine.Value,
					        new Vector2(value.X - Main.screenPosition.X + TextureAssets.FishingLine.Value.Width * 0.5f,
						        value.Y - Main.screenPosition.Y + TextureAssets.FishingLine.Value.Height * 0.5f),
					        new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0,
						        TextureAssets.FishingLine.Value.Width, (int)num)), color2, rotation2,
					        new Vector2(TextureAssets.FishingLine.Value.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
			        }
		        }

		        return false;
	        }

	        return true;
        }
    }
}