using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects.GenericEffects;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Marie {
    public class StormShockEffect : Slow {
        public override string DisplayName { get => "Shocked"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }

        private int magResLoss;

        public StormShockEffect() { }
        public StormShockEffect(float magnitude, int mrLoss, int duration, bool canBeCleansed, int applierId) : base(magnitude, duration, canBeCleansed, applierId) { 
            magResLoss = mrLoss;
        }

        public override void ConstructFlatAttributes() {
            FlatAttributes = new Dictionary<AttributeType, Func<float>> {
                { MAGICAL_ARMOR, () => magResLoss }
            };
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(magResLoss);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            magResLoss = reader.ReadInt32();
            base.ReceiveEffectElements(reader);
        }
    }
}