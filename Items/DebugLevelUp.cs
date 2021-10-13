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
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = false;
            Item.rare = 3;
            Item.color = Color.Yellow;
        }
        
        public override string Texture => "Terraria/Images/Item_" + ItemID.Ebonkoi;
        
        public override bool? UseItem(Player Player) {
            Player.GetModPlayer<MobaPlayer>().Hero.LevelUp();
            return true;
        }
    }
}