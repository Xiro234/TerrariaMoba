using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Projectiles.Nocturne;

namespace TerrariaMoba.Items.Nocturne {
    public class NocturneSword : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Nocturne's main weapon.\nA rustic sword that belonged to the Iron Division.\nVines have been wrapped around the handle and guard.");
            DisplayName.SetDefault("Serpentine");
        }
        
        public override void SetDefaults() {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 100;
            Item.knockBack = 0;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<UmbralBlade>();
            Item.shootSpeed = 6f;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
        }
    }
}