using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class ExplosiveCatch : Ability {
        public ExplosiveCatch(Player player) : base(player, "Explosive Catch", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Old Man reaches to his back pocket and flings a Bomb Fish.
        }
    }
}