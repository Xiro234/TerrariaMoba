using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class FlaskOfVitality : Ability {
        public FlaskOfVitality() : base("Flask of Vitality", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Chastradamus drinks the blood of his enemies. This heals him over time.
        }
    }
}