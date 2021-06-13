using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Nocturne {
    public class TitaniumGuardEffect: StatusEffect, IPreHurt {
        public override string DisplayName { get => "Titanium Guard"; }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }
        
        public TitaniumGuardEffect() { }
        
        public TitaniumGuardEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        public bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            return true;
        }
    }
}