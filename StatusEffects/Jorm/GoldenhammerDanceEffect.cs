using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Jorm {
    
    public class GoldenhammerDanceEffect : StatusEffect {
        public override string DisplayName { get => "Dance of the Goldenhammer"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float modifier;

        public GoldenhammerDanceEffect() { }
        public GoldenhammerDanceEffect(float magnitude, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            modifier = magnitude;
        }

        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { ATTACK_SPEED, () => -modifier },
                { MOVEMENT_SPEED, () => -modifier },
                { JUMP_SPEED, () => -modifier }
            };
        }
    }
}