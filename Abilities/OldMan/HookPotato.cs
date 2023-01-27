using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class HookPotato : Ability {
        public HookPotato(Player player) : base(player, "HookPotato", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Old Man reaches to his back pocket and flings 3 Frost Daggerfish in front of him, dealing damage and freezing.
        }
    }
}