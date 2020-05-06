using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class VerdantFury : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Verdant Fury");
            Description.SetDefault("The Jungle's power flows through you!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().VerdantFury = true;
        }
    }
}