using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
