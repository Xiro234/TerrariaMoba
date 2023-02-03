using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ConsecrationEffect : StatusEffect, IResetEffects {
        public override string DisplayName { get => "Consecration"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float modifier;
        private bool IsEnemy;
        
        public ConsecrationEffect() { }

        public ConsecrationEffect(bool enemy, float magnitude, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            modifier = magnitude;
            IsEnemy = enemy;
        }

        public void ResetEffects() { 
            /* 
             * if enemy {
             *  reduce users healing effectiveness by magnitude
             * } else {
             *  increase users healing effectiveness by magnitude
             * }
             */
        }
    }
}