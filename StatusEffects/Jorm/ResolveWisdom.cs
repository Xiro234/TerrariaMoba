using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ResolveWisdom : StatusEffect {
        public override string DisplayName { get => "Wisdom"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }
        
        private int stackCount;

        public ResolveWisdom(int stacks, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
            stackCount = stacks;
        }

        protected override Dictionary<AttributeType, Func<float>> FlatAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>> {
                { MANA_REGEN, () => stackCount * 0.5f },
                { MAGICAL_ARMOR, () => stackCount * 5f },
            };
        }
    }
}