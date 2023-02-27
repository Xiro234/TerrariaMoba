using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public sealed class VerdantFuryEffect : StatusEffect {
        public override string DisplayName { get => "Verdant Fury"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private float attackSpeed;
        private float attackVelocity;
        
        public VerdantFuryEffect() { }
        public VerdantFuryEffect(float atkspd, float atkvel, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            attackSpeed = atkspd;
            attackVelocity = atkvel;
        }
        
        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { ATTACK_SPEED, () => attackSpeed },
                { ATTACK_VELOCITY, () => attackVelocity }
            };
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(attackSpeed);
            packet.Write(attackVelocity);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            attackSpeed = reader.ReadInt32();
            attackVelocity = reader.ReadInt32();
            base.ReceiveEffectElements(reader);
        }

        public class VerdantFuryLayer : PlayerDrawLayer {
            public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
                return StatusEffectManager.PlayerHasEffectType<VerdantFuryEffect>(drawInfo.drawPlayer);
            }

            public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ProjectileOverArm,
                PlayerDrawLayers.FrozenOrWebbedDebuff);

            protected override void Draw(ref PlayerDrawSet drawInfo) {
                Player drawPlayer = drawInfo.drawPlayer;

                Texture2D texture = Mod.Assets.Request<Texture2D>("Textures/Sylvia/VerdantFuryBuff").Value;
                Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - (texture.Width / 2) - 0,
                    drawPlayer.Top.Y - Main.screenPosition.Y - 55);
                DrawData data = new DrawData(texture, texturePos, Color.White);
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }
}