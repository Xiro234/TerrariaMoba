using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;

namespace TerrariaMoba.Items {
    public class DebugGainXp : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Gains 10 Xp");
            DisplayName.SetDefault("DebugGainXp");
        }

        public override void SetDefaults() {
            item.width = 20;
            item.height = 26;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = false;
            item.rare = 3;
            item.color = Color.MediumVioletRed;
        }
        
        public override string Texture => "Terraria/Item_" + ItemID.CrimsonKey;
        
        public override bool UseItem(Player player) {
            player.GetModPlayer<MobaPlayer>().MyCharacter.GainExperience(10);
            return true;
        }
    }
}