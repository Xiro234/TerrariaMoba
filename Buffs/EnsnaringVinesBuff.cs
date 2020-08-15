using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class EnsnaringVinesBuff : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Ensnaring Vines");
            Description.SetDefault("The vines bind you!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().SylviaEffects.EnsnaringVines = true;
        }
    }
}