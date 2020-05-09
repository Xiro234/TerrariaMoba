using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;

namespace TerrariaMoba.Items {
    public class DebugLevelUp : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Levels up your character");
            DisplayName.SetDefault("DebugLevelUp");
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
            item.consumable = true;
            item.rare = 3;
            item.color = Color.Yellow;
        }
        
        public override string Texture => "Terraria/Item_" + ItemID.Ebonkoi;
        
        public override bool UseItem(Player player) {
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().MyCharacter.LevelUp();
            return true;
        }
    }
}