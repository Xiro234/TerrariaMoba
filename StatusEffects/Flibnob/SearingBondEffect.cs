using System;
using System.Collections.Generic;
using System.IO;
using log4net.Repository.Hierarchy;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public class SearingBondEffect : StatusEffect {

        public override string DisplayName { get => "Searing Bond"; }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private int currentStacks;
        private int armorGain;
        
        public SearingBondEffect() { }

        public SearingBondEffect(int stacks, int armor, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            currentStacks = stacks;
            armorGain = armor;
        }
    }
}