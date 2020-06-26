using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using TerrariaMoba.Characters;
using TerrariaMoba.Packets;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class Silenced : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Silenced");
            Description.SetDefault("You can't use abilities!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().Silenced = true;
        }
    }
}