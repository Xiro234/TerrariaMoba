using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class Floodboost : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Floodboost");
            Description.SetDefault("You feel the currents flow beneath your feet!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().MarieEffects.Floodboost = true;
        }
    }
}