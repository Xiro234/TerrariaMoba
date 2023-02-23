using Terraria.ModLoader;
using Terraria.ID;
using TerrariaMoba.Projectiles.Osteo;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using TerrariaMoba.Projectiles;

namespace TerrariaMoba.Items.Osteo {
    public class OsteoTome : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Osteo's main weapon.\nA very elegant looking book on the outside.\nAttempting to read it is highly inadvisable.");
            DisplayName.SetDefault("Forbidden Tome");
        }

        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 12;
            Item.damage = 83;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<OsteoSkull>();
            Item.UseSound = SoundID.Item104.WithVolumeScale(0.3f).WithPitchOffset(0.5f);
            Item.useTime = 57;
            Item.useAnimation = 57;
            Item.shootSpeed = 7.66f;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 0;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<OsteoSkull>(), 1, 0f, player.whoAmI);
            proj.GetGlobalProjectile<DamageTypeGlobalProj>().AutoAttack = true;

            if (MobaSystem.MatchInProgress) {
                TerrariaMobaUtils.SetProjectileDamage(proj, PhysicalDamage: (int)player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_DAMAGE));
            }

            return false;
        }
    }
}