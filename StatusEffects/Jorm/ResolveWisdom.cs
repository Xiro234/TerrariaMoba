using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class ResolveWisdom : StatusEffect, IResetEffects {
        public override string DisplayName { get => "Wisdom"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }
        
        private int stackCount;

        public ResolveWisdom(int stacks, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
            stackCount = stacks;
        }

        public void ResetEffects() {
            User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics.ResourceRegen += (stackCount * 0.5f);
            User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics.MagicalArmor += (stackCount * 5);
        }
    }
}