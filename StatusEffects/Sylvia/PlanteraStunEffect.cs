using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class PlanteraStunEffect : Stun {
        public override string DisplayName { get => "Plantera's Last Will Stun"; }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }
        
        public PlanteraStunEffect() { }

        public PlanteraStunEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
    }
}