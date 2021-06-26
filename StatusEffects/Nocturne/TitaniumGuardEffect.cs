using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Nocturne {
    public class TitaniumGuardEffect: StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Titanium Guard"; }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }
        
        public TitaniumGuardEffect() { }
        
        public TitaniumGuardEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            physicalDamage = 0;
            magicalDamage = 0; 
            trueDamage = 0;
        }
    }
}