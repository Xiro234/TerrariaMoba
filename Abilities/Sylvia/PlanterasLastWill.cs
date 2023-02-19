﻿using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;
using System;

namespace TerrariaMoba.Abilities.Sylvia {
    public class PlanterasLastWill : Ability, IModifyHitPvpWithProj {
        public PlanterasLastWill(Player player) : base(player, "Plantera's Last Will", 180, 20, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaUltimateTwo").Value; }

        public const int BULB_DAMAGE = 300;
        public const int BULB_DAMAGE_SCALING = 25;
        public const int BULB_STUN_DURATION = 50;
        public const int BULB_STUN_DURATION_SCALING = 10;

        public const int DISTANCE_SCALING_BLOCK_CAP = 200;
        public const int DISTANCE_SCALING_BLOCK_INTERVAL = 10;

        public const int SPORE_DAMAGE = 100;
        public const int SPORE_COUNT = 6;
        public const int SPORE_DURATION = 120;
        public const int SPORE_DEBUFF_DURATION = 180;
        public const float SPORE_HEALEFF_MODIFIER = -0.33f;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - User.Center;
                playerToMouse.Normalize();
                
                Vector2 position = User.Center + playerToMouse * 20;
                Vector2 velocity = playerToMouse * 7;

                Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this),position, velocity, ModContent.ProjectileType<SylviaUlt2>(), 
                    1, 0f, User.whoAmI);
                TerrariaMobaUtils.SetProjectileDamage(proj, PhysicalDamage: BULB_DAMAGE);
                
                SylviaUlt2 head = proj.ModProjectile as SylviaUlt2;
                
                if (head != null) {
                    head.SporeDamage = SPORE_DAMAGE;
                    head.NumberOfSpores = SPORE_COUNT;
                    head.SporeDuration = SPORE_DURATION;
                }

                CooldownTimer = BaseCooldown;
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref bool crit) {
            var ModProjectile = proj.ModProjectile;
            SylviaUlt2 head = ModProjectile as SylviaUlt2;
            if (head != null) {
                int damageModifier;
                int durationModifier;
                int distanceInBlocks = (int)(head.Projectile.velocity.Length() * head.Projectile.ai[0] / 16f);
                if (distanceInBlocks < DISTANCE_SCALING_BLOCK_CAP) {
                    damageModifier = (int)Math.Floor((double)(BULB_DAMAGE_SCALING * (distanceInBlocks / DISTANCE_SCALING_BLOCK_INTERVAL)));
                    durationModifier = (int)Math.Floor((double)(BULB_STUN_DURATION_SCALING * (distanceInBlocks / DISTANCE_SCALING_BLOCK_INTERVAL)));
                } else {
                    damageModifier = BULB_DAMAGE_SCALING * (DISTANCE_SCALING_BLOCK_CAP / DISTANCE_SCALING_BLOCK_INTERVAL);
                    durationModifier = BULB_STUN_DURATION_SCALING * (DISTANCE_SCALING_BLOCK_CAP / DISTANCE_SCALING_BLOCK_INTERVAL);
                }
                physicalDamage += damageModifier;
                StatusEffectManager.AddEffect(target, new PlanteraStunEffect(BULB_STUN_DURATION + durationModifier, true, User.whoAmI));
            }
            
            SylviaSpores spore = ModProjectile as SylviaSpores;
            if (spore != null) {
                StatusEffectManager.AddEffect(target, new PlanteraSporeEffect(SPORE_HEALEFF_MODIFIER, SPORE_DEBUFF_DURATION, true, User.whoAmI));
            }
        }
    }
}