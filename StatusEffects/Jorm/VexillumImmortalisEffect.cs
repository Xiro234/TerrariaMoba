using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class VexillumImmortalisEffect : StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Vexillum Immortalis"; }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public VexillumImmortalisEffect() { }

        public VexillumImmortalisEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            physicalDamage = 0;
        }
    }
}