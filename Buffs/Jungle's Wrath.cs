using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using TerrariaMoba.Characters;
using TerrariaMoba.Packets;
using TerrariaMoba.Players;
using TerrariaMoba.Utils;

namespace TerrariaMoba.Buffs {
    public class JunglesWrath : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Jungle's Wrath");
            Description.SetDefault("The jungle poisons you!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().JunglesWrath = true;
        }
        
        public override bool ReApply(Player player, int time, int buffIndex) {
            bool add = false;
            if (player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().JunglesWrathCount <= 4) {
                add = true;
                player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().JunglesWrathCount++;
            }

            player.buffTime[buffIndex] = SylviaUtils.GetJunglesWrathTime();
            SyncJunglesWrathPacket.Write(player.whoAmI, add);
            return false;
        }
    }
}