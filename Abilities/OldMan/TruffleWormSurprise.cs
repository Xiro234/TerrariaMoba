using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class TruffleWormSurprise : Ability {
        public TruffleWormSurprise() : base("Truffle Worm Surprise", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Old Man hurls a Truffle Worm to a location, and after a few seconds, the Duke himself comes up to have a little taste.
        }
    }
}