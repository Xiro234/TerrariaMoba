using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects {
    public class FunRoot : Root {
        public override string DisplayName { get => "Root"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }
        public FunRoot() { }
        public FunRoot(int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
    }
}