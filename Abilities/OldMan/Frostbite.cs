using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class Frostbite : Ability {
        public Frostbite(Player player) : base(player, "Frostbite", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Old Man reaches to his back pocket and flings 3 Frost Daggerfish in front of him, dealing damage and freezing.
        }
    }
}