using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects {
    public class FunRoot : Root {
        public override string DisplayName { get => "FunRoot"; }
        public override Texture2D Icon { get { return TerrariaMoba.Instance.GetTexture("Textures/Blank");} }
        
        public FunRoot() { }
        public FunRoot(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
    }
}