using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Items.Nocturne {
    public class NocturneSword : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Nocturne's main weapon.\nA rustic sword that belonged to the Iron Division.\nVines have been wrapped around the handle and guard.");
            DisplayName.SetDefault("Serpentine");
        }
        
        public override void SetDefaults() {
            item.width = 48;
            item.height = 48;
            item.damage = 100;
            item.knockBack = 0;
            item.melee = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("UmbralBlade");
            item.shootSpeed = 6f;
            item.useTime = 60;
            item.useAnimation = 60;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = 10000;
            item.rare = ItemRarityID.Orange;
            item.autoReuse = false;
            item.noUseGraphic = true;
        }
    }
}