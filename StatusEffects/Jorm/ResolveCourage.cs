using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ResolveCourage : StatusEffect, IResetEffects {
        public override string DisplayName { get => "Courage"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float stackCount;

        public ResolveCourage(int stacks, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
            stackCount = stacks;
        }
        
        public void ResetEffects() {
            User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics.HealthRegen += (stackCount * 0.5f);
            User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics.PhysicalArmor += (stackCount * 5f);
        }
    }
}