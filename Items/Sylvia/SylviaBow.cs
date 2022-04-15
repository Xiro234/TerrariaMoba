using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TerrariaMoba.Projectiles.Sylvia;

namespace TerrariaMoba.Items.Sylvia {
    public class SylviaBow : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Sylvia's main weapon.");
            DisplayName.SetDefault("Luscious Longbow");
        }

        public override void SetDefaults() {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<SylviaArrow>();
            Item.width = 20;
            Item.height = 12;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.UseSound = SoundID.Item5;
            Item.shootSpeed = 9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = false;
        }
        
        public override Vector2? HoldoutOffset() {
            return new Vector2(-5, 0);
        }
    }
}