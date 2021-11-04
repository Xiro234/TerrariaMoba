using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace TerrariaMoba.StatusEffects.Nocturne {
    public class RetaliationDMGEffect : StatusEffect {
        public override string DisplayName { get => "Violently Retaliating"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }
        public RetaliationDMGEffect() { }
        public RetaliationDMGEffect(float magnitude, int hits, int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        //TODO - Increase all damage Nocturne deals by (float)(magnitude*hits)
    }
}