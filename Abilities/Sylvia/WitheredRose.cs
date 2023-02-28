using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class WitheredRose : Ability, ITakePvpDamage, IDealPvpDamage {
        public WitheredRose(Player player) : base(player, "Withered Rose", 180, 20, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaAbilityTwo").Value; }

        public const int THORN_DAMAGE = 150;
        public const int THORN_MAX_LIFETIME = 360;
        public const int THORN_POISON_DAMAGE = 66;
        public const int THORN_POISON_DURATION = 120;
        public const float THORN_POISON_HEALMOD = -0.5f;
        public const int ABILITY_DURATION = 240;
        public const int NEXT_THORN_DELAY = 15;

        private int Timer = 0;
        private int ThornDelayTimer = 0;

        public override void OnCast() {
            Timer = ABILITY_DURATION;
            IsActive = true;
        }

        public override void WhileActive() {
            if (Timer == 0) {
                TimeOut();
            }

            if (ThornDelayTimer > 0) {
                ThornDelayTimer--;
            }

            const float distance = 40f;
            for (int i = 0; i < 16; i++) {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * distance);
                offset.Y += (float)(Math.Cos(angle) * distance);
                Dust dust = Main.dust[Dust.NewDust(User.Center + offset - new Vector2(4, 4), 0, 0, DustID.RichMahogany, 0, 0, 100, default, 1f)];
                dust.velocity = User.velocity;
                if (Main.rand.NextBool(3)) {
                    dust.velocity += Vector2.Normalize(offset) * -5f;
                }
                dust.noGravity = true;
            }

            Timer--;
        }

        public override void TimeOut() {
            IsActive = false;
            CooldownTimer = BaseCooldown;
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            if (IsActive && Main.player[killer].active && ThornDelayTimer == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Vector2 direction = Vector2.UnitX;
                    Vector2 velocity = direction.RotatedBy(MathHelper.ToRadians(1 * 90 + 45));

                    Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this), User.Center, velocity,
                        ModContent.ProjectileType<RoseThorn>(), 1, 0, User.whoAmI, killer);
                    TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: THORN_DAMAGE);

                    RoseThorn thorn = proj.ModProjectile as RoseThorn;
                    if (thorn != null) {
                        thorn.ThornLifetime = THORN_MAX_LIFETIME;
                    }

                    ThornDelayTimer = NEXT_THORN_DELAY;
                }
            }
        }

        public void DealPvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, Player target, DamageSource damageSource) {
            if (damageSource.source is Projectile) {
                Projectile proj = damageSource.source as Projectile;
                
                var modProj = proj.ModProjectile;
                RoseThorn thorn = modProj as RoseThorn;
                if (thorn != null) {
                    StatusEffectManager.AddEffect(target, new WitheredRoseEffect(THORN_POISON_DAMAGE, THORN_POISON_HEALMOD, THORN_POISON_DURATION, true, User.whoAmI));
                }
            }
        }
    }
}