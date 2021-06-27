using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ConsecrationEffect : StatusEffect, IResetEffects {
        public override string DisplayName { get => "Consecration"; }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

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