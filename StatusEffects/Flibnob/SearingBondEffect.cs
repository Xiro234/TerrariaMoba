using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public class SearingBondEffect : StatusEffect, IModifyHitPvpWithProj {

        public override string DisplayName { get => "Searing Bond"; }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private int currentStacks;
        private int armorGain;
        private int burnDuration;
        private int applierId;
        
        public SearingBondEffect() { }
        
        public SearingBondEffect(int id, int stacks, int armor, int burndur, int duration, bool canBeCleansed) : base(duration, false) {
            currentStacks = stacks;
            armorGain = armor;
            burnDuration = burndur;
            applierId = id;

            FlatAttributes = new Dictionary<AttributeType, Func<float>> {
                { PHYSICAL_ARMOR, () => armorGain * currentStacks }
            };
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var damageTypeProj = proj.GetGlobalProjectile<DamageTypeGlobalProj>();
            if (damageTypeProj.PhysicalDamage > 0 && proj != null) {
                if (!StatusEffectManager.PlayerHasEffectType<FlameBelchSecondEffect>(target)) {
                    StatusEffectManager.AddEffect(target, new FlameBelchEffect(applierId, burnDuration, true));
                }
            }
        }
    }
}