using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects {
    public class FunStun : Stun {
        public override string DisplayName { get => "Stun"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }
        public FunStun() { }
        public FunStun(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
    }
}