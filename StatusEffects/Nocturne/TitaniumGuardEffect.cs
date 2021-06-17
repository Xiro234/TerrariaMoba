using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Nocturne {
    public class TitaniumGuardEffect: StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Titanium Guard"; }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }
        
        public TitaniumGuardEffect() { }
        
        public TitaniumGuardEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        public void TakePvpDamage(ref int damage, ref int killer) {
            damage = 0;
        }
    }
}