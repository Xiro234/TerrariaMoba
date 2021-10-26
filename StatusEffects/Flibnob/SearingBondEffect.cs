using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public class SearingBondEffect : StatusEffect, IResetEffects {

        public override string DisplayName { get => "Searing Bond"; }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private int currentStacks;
        private int armorGain;
        
        public SearingBondEffect(int stacks, int armor, int duration, bool canBeCleansed) : base(duration, false) {
            currentStacks = stacks;
            armorGain = armor;
        }

        public void ResetEffects() {
            User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics.PhysicalArmor += armorGain * currentStacks;
        }
    }
}