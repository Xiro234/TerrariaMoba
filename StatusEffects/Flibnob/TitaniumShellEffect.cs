using Microsoft.Xna.Framework.Graphics;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public class TitaniumShellEffect : StatusEffect {
        public TitaniumShellEffect(int duration, int armor, float moveSpeed) : base(duration, false) { }
        public override string DisplayName { get => "Titanium Reflection"; }
        
        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }
        
        //TODO - Implement reflection code, armor addition and movement speed reduction.
    }
}