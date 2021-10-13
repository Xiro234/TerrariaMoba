using Terraria;
using Terraria.ModLoader;
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
            Item.color = Color.MediumVioletRed;
        }

        public override string Texture => "Terraria/Images/Item_" + ItemID.CrimsonKey;
        
        public override bool? UseItem(Player Player) {
            Player.GetModPlayer<MobaPlayer>().Hero.GainExperience(10);
            return true;
        }
    }
}