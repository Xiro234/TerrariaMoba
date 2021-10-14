using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Items.Jorm {
    public class JormHammer :  ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Jorm's main weapon.\nA light-infused hammer that only the holy can wield.\nA weapon fit for a hero.");
            DisplayName.SetDefault("Hephaestan Battlehammer");
        }
        
        public override void SetDefaults() {
            Item.width = 38;
            Item.height = 38;
            Item.damage = 99;
            Item.knockBack = 0;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ProjectileID.PaladinsHammerFriendly;
            Item.shootSpeed = 10f;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = false;
        }
        
        public override string Texture => "Terraria/Images/Item_" + ItemID.PaladinsHammer;
    }
}