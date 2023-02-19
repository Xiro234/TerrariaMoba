using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class VexillumImmortalisEffect : StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Vexillum Immortalis"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        public VexillumImmortalisEffect() { }
        public VexillumImmortalisEffect(int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
        
        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            physicalDamage *= 0;
        }
    }
}