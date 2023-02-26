using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class JunglesWrath : Ability, IDealPvpDamage  {
        public JunglesWrath(Player player) : base(player, "Jungle's Wrath", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaTrait").Value; }

        public const int EFFECT_DURATION = 300;
        public const float DAMAGE_PERCENT = 0.04f;

        public void DealPvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, Player target, DamageSource damageSource) {
            if (damageSource.source is Projectile) {
                Projectile proj = damageSource.source as Projectile;

                var ModProjectile = proj.ModProjectile;
                SylviaArrow arrow = ModProjectile as SylviaArrow;

                if (arrow != null) {
                    StatusEffectManager.AddEffect(target,
                        new JunglesWrathEffect(EFFECT_DURATION, User.whoAmI, DAMAGE_PERCENT, 1));
                }
            }
        }
    }
}