using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ResolveCourage : StatusEffect {
        public override string DisplayName { get => "Courage"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float stackCount;

        public ResolveCourage() { }

        public ResolveCourage(int stacks, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            stackCount = stacks;
            
            FlatAttributes = new Dictionary<AttributeType, Func<float>> {
                { PHYSICAL_ARMOR, () => stackCount * 5f },
                { HEALTH_REGEN, () => stackCount * 0.5f },
            };
        }
    }
}