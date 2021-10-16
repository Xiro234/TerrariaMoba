using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class PerfectFillet : Ability {
        public PerfectFillet() : base("Perfect Fillet", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Old Man prepares a lovely bass fillet which starts to cook for 6s. Allies that walk into this are healed for XHP. Enemies destroy it upon contact.
        }
    }
}