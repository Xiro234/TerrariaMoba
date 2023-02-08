using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
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

        public VerdantFuryEffect(int duration, float atkspd, float atkvel, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            attackSpeed = atkspd;
            attackVelocity = atkvel;
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
        
        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { ATTACK_SPEED, () => attackSpeed },
                { ATTACK_VELOCITY, () => attackVelocity }
            };
        }
    }
}