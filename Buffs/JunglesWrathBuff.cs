using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using TerrariaMoba.Characters;
using TerrariaMoba.Packets;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class JunglesWrathBuff : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Jungle's Wrath");
            Description.SetDefault("The jungle poisons you!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
            canBeCleared = false;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().SylviaEffects.JunglesWrath = true;
        }
    }
}