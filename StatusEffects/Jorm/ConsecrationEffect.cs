using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
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
                { HEALING_EFFECTIVENESS, () => 1 - healingModifier }
            };
        }
    }
}