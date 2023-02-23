using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Network;

namespace TerrariaMoba.StatusEffects.Osteo {
    public class EyeOfFrightEffect : StatusEffect, ISetControls {
        public override string DisplayName { get => "Eye of Fright Fear"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value; } }

        private float velocityToOsteo;

        public EyeOfFrightEffect() { }
        public EyeOfFrightEffect(float vel, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            velocityToOsteo = vel;
        }

        public override void WhileActive() {
            Vector2 direction = Main.player[ApplicantID].Center - User.Center;
            direction.Normalize();
            User.velocity = direction * velocityToOsteo;
            NetworkHandler.SendSyncPlayerVelocity(User.whoAmI, User.velocity);

            base.WhileActive();
        }

        public void SetControls() {
            User.controlRight = false;
            User.controlLeft = false;
            User.controlJump = false;
            User.controlUp = false;
            User.controlDown = false;
            User.controlUseItem = false;
            User.controlHook = false;
            User.controlInv = false;
            User.controlMap = false;
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(velocityToOsteo);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            velocityToOsteo = reader.ReadSingle();
            base.ReceiveEffectElements(reader);
        }

        public class EyeOfFrightLayer : PlayerDrawLayer {
            public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
                return StatusEffectManager.PlayerHasEffectType<EyeOfFrightEffect>(drawInfo.drawPlayer);
            }

            public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ProjectileOverArm,
                PlayerDrawLayers.FrozenOrWebbedDebuff);

            protected override void Draw(ref PlayerDrawSet drawInfo) {
                Player drawPlayer = drawInfo.drawPlayer;

                Texture2D texture = Mod.Assets.Request<Texture2D>("Textures/Osteo/EOFFear").Value;
                Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - (texture.Width / 2) - 0,
                    drawPlayer.Top.Y - Main.screenPosition.Y - 55);
                DrawData data = new DrawData(texture, texturePos, Color.White);
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }
}
