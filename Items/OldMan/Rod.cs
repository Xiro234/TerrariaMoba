using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.OldMan;

namespace TerrariaMoba.Items.OldMan {
    public class Rod : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("rod");
            DisplayName.SetDefault("rod");
        }

        public override void SetDefaults() {
            Item.damage = 69;
            Item.shoot = ModContent.ProjectileType<Bobber>();
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item5;
            Item.shootSpeed = 9f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback) {
            Projectile proj = Projectile.NewProjectileDirect(source, position + new Vector2(47, -31), velocity, 
                ModContent.ProjectileType<Bobber>(), 1, 0f, player.whoAmI);
            proj.GetGlobalProjectile<DamageTypeGlobalProj>().AutoAttack = true;

            return false;
        }

        public override void UseItemFrame(Player player) {
            player.bodyFrame.Y = 3 * 56;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) {
            if (player.itemTime <= 2) {
                Item.useStyle = ItemUseStyleID.HoldUp;
                //offs
            }
            else {
                Item.useStyle = ItemUseStyleID.Swing;
            }
        }
    }
}