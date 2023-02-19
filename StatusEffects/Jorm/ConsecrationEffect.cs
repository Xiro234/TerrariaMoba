using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ConsecrationEffect : StatusEffect {
        public override string DisplayName { get => "Consecration"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float healingModifier;
        
        public ConsecrationEffect() { }
        public ConsecrationEffect(float healMod, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            healingModifier = healMod;
        }

        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { HEALING_EFFECTIVENESS, () => healingModifier }
            };
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(healingModifier);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            healingModifier = reader.ReadSingle();
            base.ReceiveEffectElements(reader);
        }
    }
}