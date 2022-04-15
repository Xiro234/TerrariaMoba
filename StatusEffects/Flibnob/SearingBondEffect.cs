using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public class SearingBondEffect : StatusEffect {

        public override string DisplayName { get => "Searing Bond"; }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private int currentStacks;
        private int armorGain;
        
        public SearingBondEffect() { }
        
        public SearingBondEffect(int stacks, int armor, int duration, bool canBeCleansed) : base(duration, false) {
            currentStacks = stacks;
            armorGain = armor;
            
            FlatAttributes = new Dictionary<AttributeType, Func<float>> {
                { PHYSICAL_ARMOR, () => armorGain * currentStacks }
            };
        }
    }
}