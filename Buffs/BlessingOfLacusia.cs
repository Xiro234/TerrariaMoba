﻿using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class BlessingOfLacusia : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Blessing of Lacusia");
            Description.SetDefault("You have been blessed by the Goddess!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().LacusianBlessing = true;
            player.statDefense += 12;
            player.lifeRegen += 8;
            player.allDamageMult += (float)0.16;
        }
    }
}