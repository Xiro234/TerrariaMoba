using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class WhatsInTheCrate : Ability {
        public WhatsInTheCrate() : base("What's in the Crate?", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Old Man rummages through his Golden Crate to decide which primary weapon he should bring to today's spar.
        }

        public override void WhileActive() {
            
        }
    }
}