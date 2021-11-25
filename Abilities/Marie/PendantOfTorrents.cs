using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Marie {
    public class PendantOfTorrents : Ability {
        public PendantOfTorrents(Player player) : base(player, "Pendant of Torrents", 60, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieTrait").Value; }

        public override void WhileActive() {
            //TODO - magic dmg dealt to enemies can reduce their MR, healing allies makes their basic attacks deal bonus magical dmg
        }
    }
}