using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Network;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles.OldMan {
    public class Bobber : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("OldManBobber");
        }
        
        private enum AIState {
	        Casting,
	        Retract,
	        Attached
        }
	    
        public float BobberMultiplier { get; set; }
        public int BobberCatchTime { get; set; }
        public int BobberDuration { get; set; }

        private AIState CurrentAIState {
	        get => (AIState)Projectile.ai[0];
	        set => Projectile.ai[0] = (float)value;
        }

        private float BobberTimer {
	        get => Projectile.ai[1];
	        set => Projectile.ai[1] = (float)value;
        }

        private int attachedPlayerID { get; set; }

        public override void SetDefaults() {
            Projectile.friendly = true;
            DrawOriginOffsetY = -8;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            attachedPlayerID = -1;

            BobberMultiplier = 1.5f;
            BobberCatchTime = 60 * 3;
            BobberDuration = 60 * 6;
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
			        
			        HoldProj();
			        
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

			        HoldProj();
			        
			        if (Main.LocalPlayer == player && Main.mouseRight) {
				        CurrentAIState = AIState.Retract;
				        Projectile.netUpdate = true;
				        break;
			        }

			        if (CheckToAttach()) {
				        CurrentAIState = AIState.Attached;
				        Projectile.netUpdate = true;
			        }
			        
			        break;
		        }

		        case AIState.Attached: {
			        Player attachedPlayer = Main.player[attachedPlayerID];
			        Projectile.Center = attachedPlayer.Center;
			        Projectile.velocity = Vector2.Zero;

			        if (BobberTimer >= BobberDuration) { //Over Duration
				        Detach();
				        break;
			        }

			        if (Main.LocalPlayer == player && Main.mouseLeft) {
				        if (BobberTimer >= BobberCatchTime) { //Multiplier Window
							//attachedPlayer.GetModPlayer<MobaPlayer>().TakePvpDamage((int)Math.Floor(10 * BobberMultiplier), 0, 0, player.whoAmI, false);

							Vector2 direction = (player.Center - attachedPlayer.Center);
							direction.Normalize();

							int magnitude = 8;

							attachedPlayer.velocity += direction * magnitude;
							NetworkHandler.SendSyncPlayerVelocity(attachedPlayerID, attachedPlayer.velocity);
							Detach();
				        }
				        else { //Before Multiplier
					        //attachedPlayer.GetModPlayer<MobaPlayer>().TakePvpDamage(10, 0, 0, player.whoAmI, false);
					        Detach();
				        }
			        }

			        HoldProj();
			        
			        if (Main.LocalPlayer == player && Main.mouseRight) {
				        CurrentAIState = AIState.Retract;
				        Projectile.netUpdate = true;
				        attachedPlayerID = -1;
				        BobberTimer = 0;
			        }
			        
			        BobberTimer += 1;
			        break;
		        }
	        }
        }

        private void HoldProj() {
	        Player player = Main.player[Projectile.owner];

	        player.heldProj = Projectile.whoAmI;

	        if (player.itemTime <= 2) {
		        player.SetDummyItemTime(2);
	        }
        }

        private bool CheckToAttach() {
	        foreach (var Player in Main.player) {
		        if(Player != null && Player.active){
			        if (Projectile.Hitbox.Intersects(Player.Hitbox) && Player.team != Main.player[Projectile.owner].team) {
				        Attach(Player);
				        return true;
			        }

		        }
	        }

	        return false;
        }

        private void Attach(Player player) {
	        attachedPlayerID = player.whoAmI;
	        BobberTimer = 0;
        }

        private void Detach() {
	        attachedPlayerID = -1;
	        BobberTimer = 0;
	        CurrentAIState = AIState.Retract;
	        Projectile.netUpdate = true;
        }

        public override void SendExtraAI(BinaryWriter writer) {
	        writer.Write(attachedPlayerID);
	        writer.Write(BobberMultiplier);
	        writer.Write(BobberCatchTime);
	        writer.Write(BobberDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
	        attachedPlayerID = reader.ReadInt32();
	        BobberMultiplier = reader.ReadSingle();
	        BobberCatchTime = reader.ReadInt32();
	        BobberDuration = reader.ReadInt32();
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers,
	        List<int> overWiresUI) {
	        overPlayers.Add(index);
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

        public override void PostDraw(Color lightColor) {
	        if (CurrentAIState == AIState.Attached) {
		        if (BobberTimer > BobberCatchTime) {
			        Texture2D texture = ModContent.Request<Texture2D>("TerrariaMoba/Textures/OldMan/Catch").Value;
			        Rectangle frame = texture.Frame();
			        Vector2 origin = new Vector2(frame.Width / 2, frame.Height / 2);
			        Vector2 pos = Main.player[attachedPlayerID].Center - Main.screenPosition + new Vector2(0, -60f);
			        
			        Main.EntitySpriteDraw(texture, pos, frame, lightColor, 0f, origin, Vector2.One, SpriteEffects.None, 0);
		        }
	        }
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