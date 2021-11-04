using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace TerrariaMoba.StatusEffects.Marie {
    public class ConfluenceEffect : StatusEffect {
        public override string DisplayName { get => "Root"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }
        public ConfluenceEffect() { }
        public ConfluenceEffect(int stacks, int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        //TODO - Increase magic damage by (5*stacks)%.
    }
}