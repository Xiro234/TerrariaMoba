using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects {
    public class FunSlow : Slow {
        public override string DisplayName { get => "Slow"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }
        public FunSlow() { }
        public FunSlow(float magnitude, int duration, bool canBeCleansed) : base(magnitude, duration, canBeCleansed) { }
    }
}