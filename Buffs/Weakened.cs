using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using TerrariaMoba.Players;
using TerrariaMoba.Stats;

namespace TerrariaMoba.Buffs {
    public class Weakened : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Weakened");
            Description.SetDefault("You do less damage!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().Weakened = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare) {
            var player = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            
            float damage = 0f;
            foreach (var item in player.ReducedDamageList) {
                damage += item.Item2;
            }

            tip = "You do " + damage * 100 + "% less damage!";
        }
    }
}