using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects {
    public class FunStun : Stun {
        public override string DisplayName { get => "FunStun"; }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }
        
        public FunStun() { }
        public FunStun(int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
        }
    }
}