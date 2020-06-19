using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs
{
    public class Channeling : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Channeling...");
            Description.SetDefault("Your spirit is channeling its energy to cast this spell for you, just be patient...");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().IsChanneling = true;
        }
    }
}