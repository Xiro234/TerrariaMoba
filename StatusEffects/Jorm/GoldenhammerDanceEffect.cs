using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects.GenericEffects;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Jorm {
    
    public class GoldenhammerDanceEffect : Daze {
        public override string DisplayName { get => "Dance of the Goldenhammer"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }
        
        public GoldenhammerDanceEffect() { }
        public GoldenhammerDanceEffect(float magnitude, int duration, bool canBeCleansed, int applierId) : base(magnitude, duration, canBeCleansed, applierId) { }
    }
}