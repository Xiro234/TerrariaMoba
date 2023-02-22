using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Osteo;

namespace TerrariaMoba.Abilities.Osteo {
    public class LifedrainPulse : Ability, IModifyHitPvpWithProj {
        public LifedrainPulse(Player player) : base(player, "Lifedrain Pulse", 180, 25, AbilityType.Active) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoAbilityTwo").Value; }

        public const int PULSE_DAMAGE = 300;
        public const int PULSE_DELAY = 90;
        public const int PULSE_LIFETIME = 90;
        public const int PULSE_WAVE_COUNT = 8;
        public const float PULSE_WAVE_SPEED = 6.5f;
        public const float PULSE_THREE_DAMAGE_MODIFIER = 0.50f;
        public const float PULSE_THREE_HEALING_MODIFIER = 0.50f;

        private int Timer;
        private int PulseCount;

        public override void OnCast() {
            Timer = 0;
            PulseCount = 0;
            IsActive = true;
        }

        public override void WhileActive() {
            if (Timer > 0) {
                Timer--;
            }
            
            if (Timer == 0) {
                if (PulseCount == 2) {
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                        for (int i = 0; i < PULSE_WAVE_COUNT; i++) {
                            double x = Math.Cos(((Math.PI / 180) * ((360f / PULSE_WAVE_COUNT) * i)));
                            double y = Math.Sin(((Math.PI / 180) * ((360f / PULSE_WAVE_COUNT) * i)));
                            Vector2 direction = new Vector2((float)x, (float)y);
                            Vector2 position = User.Center + direction * 16;
                            Vector2 velocity = direction * PULSE_WAVE_SPEED;
                            Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this),
                                position, velocity, ModContent.ProjectileType<LifedrainPulseThird>(), 1,
                                0, User.whoAmI);
                            TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: (int)(PULSE_DAMAGE * (1 + PULSE_THREE_DAMAGE_MODIFIER)));
                            proj.timeLeft = PULSE_LIFETIME;
                        }
                    }

                    TimeOut();
                } else {
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                        for (int i = 0; i < PULSE_WAVE_COUNT; i++) {
                            double x = Math.Cos(((Math.PI / 180) * ((360f / PULSE_WAVE_COUNT) * i)));
                            double y = Math.Sin(((Math.PI / 180) * ((360f / PULSE_WAVE_COUNT) * i)));
                            Vector2 direction = new Vector2((float)x, (float)y);
                            Vector2 position = User.Center + direction * 16;
                            Vector2 velocity = direction * PULSE_WAVE_SPEED;
                            Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this),
                                position, velocity, ModContent.ProjectileType<Projectiles.Osteo.LifedrainPulse>(), 1,
                                0, User.whoAmI);
                            TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: PULSE_DAMAGE);
                            proj.timeLeft = PULSE_LIFETIME;
                        }
                    }

                    Timer = PULSE_DELAY;
                    PulseCount++;
                }
            }
        }

        public override void TimeOut()  {
            IsActive = false;
            CooldownTimer = BaseCooldown;
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref bool crit) {
            var modProj = proj.ModProjectile;
            LifedrainPulseThird bigPulse = modProj as LifedrainPulseThird;
            if (bigPulse != null) {
                int finalDamage = (int)Math.Ceiling(magicalDamage - (magicalDamage * target.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(Statistic.AttributeType.MAGICAL_ARMOR) * 0.01f));
                User.GetModPlayer<MobaPlayer>().HealMe((int)Math.Ceiling(finalDamage * PULSE_THREE_HEALING_MODIFIER), true);
            }
        }
    }
}