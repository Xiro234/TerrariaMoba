﻿using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Marie
{
    public class MarieTraitEffect : StatusEffect, IModifyHitPvpWithProj {

        public override string DisplayName { get => "Goddess's Blessing"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value; } }

        private int damageIncrease;

        public MarieTraitEffect() { }
        public MarieTraitEffect(int magicDamage, int duration, bool canBeCleansed) : base(duration, canBeCleansed) { 
            damageIncrease = magicDamage;
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            Main.NewText("You have the Goddess's Blessing, this should increase your magic damage by " + damageIncrease + " but not right now!");
        }
    }
}
