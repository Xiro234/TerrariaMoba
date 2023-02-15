using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace TerrariaMoba.StatusEffects.Marie {
    public class StormWetEffect : StatusEffect {
        public override string DisplayName { get => "Root"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }
        public StormWetEffect() { }
        public StormWetEffect(float reduction, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
        
        //TODO - Enemy status resistance is reduced by 'reduction'. Can go negative, increasing debuff duration.
    }
}