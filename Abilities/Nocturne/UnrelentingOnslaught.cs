using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class UnrelentingOnslaught : Ability {
        public UnrelentingOnslaught() : base("Unrelenting Onslaught", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Nocturne swings down Serpentine and then thrusts it forward in one clean motion dealing damage.
        }
    }
}