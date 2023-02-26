using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Flibnob;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Abilities.Flibnob {
    public class SearingBond : Ability, IDealPvpDamage {
        public SearingBond(Player player) : base(player, "Searing Bond", 0, 0, AbilityType.Active) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobTrait").Value; }

        public const int BASE_ARMOR_GAIN = 5;
        public const int BURN_BASE_DURATION = 120;
        public const float BUFF_RANGE = 30f;
        private int finalStacks;

        public override void OnCast() {
            if (!IsActive) {
                IsActive = true;
            }
        }

        public override void WhileActive() {
            int total = 0;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (plr.Center - User.Center).Length() / 16.0f;
                if (plr.active && plr.team != User.team && dist <= BUFF_RANGE && i != User.whoAmI) { // && plr.whatever.hasEffect("fire")
                    total++;
                }
            }
            finalStacks = total;

            ConstructFlatAttributes();
        }

        public void DealPvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, Player target, DamageSource damageSource) {
            if (damageSource.source is Projectile) {
                Projectile proj = damageSource.source as Projectile;

                if (proj.owner == User.whoAmI) {
                    var dmgType = proj.GetGlobalProjectile<DamageTypeGlobalProj>();
                    if (proj != null && dmgType.PhysicalDamage > 0) {
                        StatusEffectManager.AddEffect(target,
                            new FlameBelchEffect(10, BURN_BASE_DURATION, true, User.whoAmI));
                    }
                }
            }
        }
        
        public override void ConstructFlatAttributes() {
            PassiveFlatAttributes = new Dictionary<AttributeType, Func<float>> {
                { PHYSICAL_ARMOR, () => BASE_ARMOR_GAIN * finalStacks }
            };
        }
    }
}