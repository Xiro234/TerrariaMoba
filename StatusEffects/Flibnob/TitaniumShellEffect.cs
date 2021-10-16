using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public class TitaniumShellEffect : StatusEffect {
        public TitaniumShellEffect(int duration, int armor, float moveSpeed) : base(duration, false) { }
        public override string DisplayName { get => "Titanium Reflection"; }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }
        
        //TODO - Implement reflection code, armor addition and movement speed reduction.
    }
}