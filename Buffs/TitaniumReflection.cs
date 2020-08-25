using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class TitaniumReflection : ModBuff { 
        public override void SetDefaults() {
            DisplayName.SetDefault("Titanium Reflection");
            Description.SetDefault("You are encased in a Titanium Shell!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }
        
        public override void Update(Player player, ref int buffIndex) {
            //player.GetModPlayer<MobaPlayer>().FlibnobEffects.TitaniumShell = true;
        }
    }
}