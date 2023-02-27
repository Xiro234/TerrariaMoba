using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class PlanteraSporeEffect : StatusEffect {
        public override string DisplayName { get => "Spore"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private float healingReduction;

        public PlanteraSporeEffect() { }
        public PlanteraSporeEffect(float healMod, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { 
            healingReduction = healMod;
        }

        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { HEALING_EFFECTIVENESS, () => healingReduction }
            };
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(healingReduction);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            healingReduction = reader.ReadSingle();
            base.ReceiveEffectElements(reader);
        }

        public class PlanteraSporeLayer : PlayerDrawLayer  {
            public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
                return StatusEffectManager.PlayerHasEffectType<PlanteraSporeEffect>(drawInfo.drawPlayer);
            }

            public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ProjectileOverArm,
                PlayerDrawLayers.FrozenOrWebbedDebuff);

            protected override void Draw(ref PlayerDrawSet drawInfo) {
                Player drawPlayer = drawInfo.drawPlayer;

                Texture2D texture = Mod.Assets.Request<Texture2D>("Textures/Sylvia/PlanteraSporePoison").Value;
                Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - (texture.Width / 2) - 0,
                    drawPlayer.Top.Y - Main.screenPosition.Y - 55);
                DrawData data = new DrawData(texture, texturePos, Color.White);
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }
}
