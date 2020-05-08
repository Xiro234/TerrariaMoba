using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.Items.Sylvia {
    public class SylviaBow : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Sylvia's main weapon.");
            DisplayName.SetDefault("Luscious Longbow");
        }

        public override void SetDefaults() {
            item.damage = 10;
            item.ranged = true;
            item.shoot = mod.ProjectileType("SylviaArrow");
            item.width = 20;
            item.height = 12;
            item.useTime = 20;
            item.UseSound = SoundID.Item5;
            item.shootSpeed = 10;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 0;
            item.value = 10000;
            item.rare = 2;
            item.autoReuse = false;
        }
        
        public override Vector2? HoldoutOffset() {
            return new Vector2(-5, 0);
        }
    }
}