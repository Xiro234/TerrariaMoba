using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class HumorReblance : Ability {
        public HumorReblance(Player player) : base(player, "Humor Reblance", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Chastradamus targets an enemy hero and applies leeches to their wounds. Chastradamus gains blood over time quickly as the leeches are attached. The leeches inflict a slowing effect and silencing for a duration.
        }
    }
}