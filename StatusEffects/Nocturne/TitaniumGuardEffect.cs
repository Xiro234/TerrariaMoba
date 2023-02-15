using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.StatusEffects.Nocturne {
    public class TitaniumGuardEffect: StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Titanium Guard"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }
        
        public TitaniumGuardEffect() { }
        
        public TitaniumGuardEffect(int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
        
        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            physicalDamage = 0;
            magicalDamage = 0; 
            trueDamage = 0;
        }
    }
}