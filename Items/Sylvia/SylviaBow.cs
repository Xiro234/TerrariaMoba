using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.Statistic;
using System.Collections.Generic;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;
using System;

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

                var mobaPlr = player.GetModPlayer<MobaPlayer>();
                foreach (var effect in new List<StatusEffect>(mobaPlr.EffectList)) {
                    if (effect is VerdantFuryEffect) {
                        const float distance = 40f;
                        for (int i = 0; i < 12; i++) {
                            Vector2 offset = new Vector2();
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            offset.X += (float)(Math.Sin(angle) * distance);
                            offset.Y += (float)(Math.Cos(angle) * distance);
                            Dust dust = Main.dust[Dust.NewDust(Item.Center + offset, 0, 0, DustID.Enchanted_Gold, 0, 0, 130, Color.LightGreen, 1f)];
                            dust.velocity += Vector2.Normalize(offset) * -6.66f;
                            dust.noGravity = true;
                        }
                    }
                }
            }

            return false;
        }
    }
}