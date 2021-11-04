using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace TerrariaMoba.StatusEffects.Marie {
    public class StormShockEffect : StatusEffect {
        public override string DisplayName { get => "Root"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }
        public StormShockEffect() { }
        public StormShockEffect(float magnitude, int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        //TODO - Enemy takes 'magnitude' more damage from all sources.
    }
}