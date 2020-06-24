using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs
{
    public class BattleHardened : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Battle Hardened");
            Description.SetDefault("Flibnob never feel pain!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }
        
        public override void Update(Player player, ref int buffIndex) {
            //player.GetModPlayer<MobaPlayer>().BattleHardened = true;
        }
        
        public override void ModifyBuffTip(ref string tip, ref int rare) {
            tip += "\nDefense Boost: " + Main.LocalPlayer.statDefense;
            rare = 10;
        }
    }
}