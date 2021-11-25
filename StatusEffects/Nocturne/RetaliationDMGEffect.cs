using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Nocturne {
    public class RetaliationDMGEffect : StatusEffect {
        public override string DisplayName { get => "Violently Retaliating"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private float dmgBoost;
        private int hitsTaken;

        public RetaliationDMGEffect() { }

        public RetaliationDMGEffect(float magnitude, int hits, int duration, bool canBeCleansed) : base(duration,
            canBeCleansed) {
            dmgBoost = magnitude;
            hitsTaken = hits;
        }
        
        protected override Dictionary<AttributeType, Func<float>> MultAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { ATTACK_DAMAGE, () => dmgBoost * hitsTaken }
            };
        }
        
        //TODO - Increase Nocturne's ability damage.
    }
}