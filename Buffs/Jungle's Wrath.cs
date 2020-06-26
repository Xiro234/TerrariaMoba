using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using TerrariaMoba.Characters;
using TerrariaMoba.Packets;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class JunglesWrath : ModBuff {
        public int stacks = 1;
        
        public override void SetDefaults() {
            DisplayName.SetDefault("Jungle's Wrath");
            Description.SetDefault("The jungle poisons you!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().lifeDegen += 2;
        }
    }
}