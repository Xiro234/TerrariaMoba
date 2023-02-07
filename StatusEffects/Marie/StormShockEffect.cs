using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Marie {
    public class StormShockEffect : StatusEffect {
        public override string DisplayName { get => "Root"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }

        private float slowMagnitude;
        private int magResLoss;

        public StormShockEffect() { }

        public StormShockEffect(float magnitude, int mrLoss, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { 
            slowMagnitude = magnitude;
            magResLoss = mrLoss;
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(slowMagnitude);
            packet.Write(magResLoss);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            slowMagnitude = reader.ReadSingle();
            magResLoss = reader.ReadInt32();
            base.ReceiveEffectElements(reader);
        }

        public override void ConstructFlatAttributes() {
            FlatAttributes = new Dictionary<AttributeType, Func<float>> {
                { MAGICAL_ARMOR, () => -magResLoss }
            };
        }

        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { MOVEMENT_SPEED, () => -slowMagnitude }
            };
        }
    }
}