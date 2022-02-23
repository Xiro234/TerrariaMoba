using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ConsecrationEffect : StatusEffect, IResetEffects {
        public override string DisplayName { get => "Consecration"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float modifier;
        private bool IsEnemy;
        
        public ConsecrationEffect() { }

        public ConsecrationEffect(bool enemy, float magnitude, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
            modifier = magnitude;
            IsEnemy = enemy;
        }

        public void ResetEffects() {
            //User.GetModPlayer<MobaPlayer>().Stats.HealEff += modifier;
            //if(IsEnemy) { StatusEffectManager.RemoveEffect(User, beneficial buffs) }
        }
    }
}