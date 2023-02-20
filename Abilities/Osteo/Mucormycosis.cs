using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Osteo;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Abilities.Osteo {
    public class Mucormycosis : Ability, IModifyHitPvpWithProj {
        public Mucormycosis(Player player) : base(player, "Mucormycosis", 60, 0, AbilityType.Passive) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoTrait").Value; }

        public const int DEBUFF_DURATION = 180;
        public const int MUCOR_SPORE_DAMAGE = 250;
        public const int MUCOR_SPORE_DURATION = 300;
        public const int MUCOR_POISON_DAMAGE = 69;
        public const int MUCOR_POISON_DURATION = 120;
        public const float HEALING_MODIFIER = -0.5f;

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int phyiscalDamage, ref int magicalDamage, ref int trueDamage, ref bool crit) {
            if (proj != null && proj.owner == User.whoAmI) {
                StatusEffectManager.AddEffect(target, 
                    new MucormycosisEffect(MUCOR_SPORE_DAMAGE, MUCOR_SPORE_DURATION, MUCOR_POISON_DAMAGE, MUCOR_POISON_DURATION, DEBUFF_DURATION, true, User.whoAmI));
            }
        }

        public override void ConstructMultAttributes() {
            PassiveMultAttributes = new Dictionary<AttributeType, Func<float>> {
                { HEALTH_REGEN, () => 0 },
                { HEALING_EFFECTIVENESS, () => HEALING_MODIFIER }
            };
        }
    }
}