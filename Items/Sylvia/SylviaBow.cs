﻿using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Items.Sylvia {
    public class SylviaBow : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Sylvia's main weapon.");
            DisplayName.SetDefault("Luscious Longbow");
        }

        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 12;
            Item.damage = 69;
            Item.DamageType = DamageClass.Ranged;
            Item.shoot = ModContent.ProjectileType<SylviaArrow>();
            Item.autoReuse = true;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.shootSpeed = 1f;
            Item.knockBack = 0;
            Item.noMelee = true;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
        }
        
        public override Vector2? HoldoutOffset() {
            return new Vector2(-5, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<SylviaArrow>(), 1, 0f, player.whoAmI);
            proj.GetGlobalProjectile<DamageTypeGlobalProj>().AutoAttack = true;
            
            if (MobaSystem.MatchInProgress) {
                TerrariaMobaUtils.SetProjectileDamage(proj, PhysicalDamage: (int)player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_DAMAGE));
            }

            return false;
        }
    }
}