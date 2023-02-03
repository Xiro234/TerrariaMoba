using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Marie {
    public class ConfluenceEffect : StatusEffect {

        public override string DisplayName { get => "Confluence"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }

        private float moveSpeed;

        public ConfluenceEffect() { }
        public ConfluenceEffect(float ms, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { 
            moveSpeed = ms;
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(moveSpeed);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            moveSpeed = reader.ReadSingle();
            base.ReceiveEffectElements(reader);
        }

        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { MOVEMENT_SPEED, () => moveSpeed }
            };
        }
    }
}