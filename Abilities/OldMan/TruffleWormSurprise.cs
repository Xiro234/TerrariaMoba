using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.OldMan {
    public class TruffleWormSurprise : Ability {
        public TruffleWormSurprise(Player player) : base(player, "Truffle Worm Surprise", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock").Value; }

        public override void OnCast() {
            //TODO - Old Man hurls a Truffle Worm to a location, and after a few seconds, the Duke himself comes up to have a little taste.
        }
    }
}