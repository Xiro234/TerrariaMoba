using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Items.Jorm {
    public class JormHammer :  ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Jorm's main weapon.\nA light-infused hammer that only the holy can wield.\nA weapon fit for a hero.");
            DisplayName.SetDefault("Hephaestan Battlehammer");
        }
        
        public override void SetDefaults() {
            item.width = 38;
            item.height = 38;
            item.damage = 99;
            item.knockBack = 0;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.shoot = ProjectileID.PaladinsHammerFriendly;
            item.shootSpeed = 10f;
            item.useTime = 60;
            item.useAnimation = 60;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = 10000;
            item.rare = ItemRarityID.Orange;
            item.autoReuse = false;
        }
        
        public override string Texture => "Terraria/Item_" + ItemID.PaladinsHammer;
    }
}