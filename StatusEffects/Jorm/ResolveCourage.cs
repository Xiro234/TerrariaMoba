﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ResolveCourage : StatusEffect {
        public override string DisplayName { get => "Courage"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float stackCount;

        public ResolveCourage(int stacks, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
            stackCount = stacks;
        }
        //TODO - Think of easy way to get attributes easy
        public override Dictionary<AttributeType, float> FlatAttributes { get => {
            { PHYSICAL_ARMOR, stackCount * 0.5f },
            { HEALTH_REGEN, stackCount * 0.5f },
        };
    }
}