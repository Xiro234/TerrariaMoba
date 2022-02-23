using System;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Items {
    public class MobaGlobalItem : GlobalItem {
        public override void SetDefaults(Item item) {
            var player = Main.LocalPlayer;
            item.useAnimation = (int)Math.Floor(player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_SPEED));
            item.useTime = item.useAnimation;
        }
    }
}