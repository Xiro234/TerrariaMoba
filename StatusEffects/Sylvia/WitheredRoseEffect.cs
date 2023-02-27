using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects.GenericEffects;
using static TerrariaMoba.Statistic.AttributeType;
using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using System.IO;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class WitheredRoseEffect : DamageOverTime {
        public override string DisplayName { get => "Poison"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private float modifier;

        public WitheredRoseEffect() { }
        public WitheredRoseEffect(int dmg, float healMod, int duration, bool canBeCleansed, int applierId) : base(0, dmg, 0, duration, canBeCleansed, applierId) {
            modifier = healMod;
        }

        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { HEALING_EFFECTIVENESS, () => modifier }
            };
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(modifier);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            modifier = reader.ReadSingle();
            base.ReceiveEffectElements(reader);
        }

        public class WitheredRoseLayer : PlayerDrawLayer {
            public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
                return StatusEffectManager.PlayerHasEffectType<WitheredRoseEffect>(drawInfo.drawPlayer);
            }

            public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ProjectileOverArm,
                PlayerDrawLayers.FrozenOrWebbedDebuff);

            protected override void Draw(ref PlayerDrawSet drawInfo) {
                Player drawPlayer = drawInfo.drawPlayer;

                Texture2D texture = Mod.Assets.Request<Texture2D>("Textures/Sylvia/WitheredRosePoison").Value;
                Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - (texture.Width / 2) - 0,
                    drawPlayer.Top.Y - Main.screenPosition.Y - 55);
                DrawData data = new DrawData(texture, texturePos, Color.White);
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }
}
