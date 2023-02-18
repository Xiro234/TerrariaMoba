using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Abilities.Sylvia {
    public class WitheredRose : Ability, ITakePvpDamage, IModifyHitPvpWithProj {
        public WitheredRose(Player player) : base(player, "Withered Rose", 60, 1, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaAbilityTwo").Value; }

        public const int THORN_DAMAGE = 150;
        public const int THORN_MAX_LIFETIME = 600;
        public const int THORN_POISON_DAMAGE = 66;
        public const int THORN_POISON_DURATION = 120;
        public const float THORN_POISON_HEALMOD = 0.5f;
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
            Timer--;
        }

        public override void TimeOut() {
            IsActive = false;
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            if (IsActive && Main.player[killer].active && ThornDelayTimer == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Vector2 direction = Vector2.UnitX;
                    Vector2 velocity = direction.RotatedBy(MathHelper.ToRadians(1 * 90 + 45));

                    Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this), User.Center, velocity,
                        ModContent.ProjectileType<RoseThorn>(), 1, 0, User.whoAmI, killer);
                    TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: THORN_DAMAGE);

                    ThornDelayTimer = NEXT_THORN_DELAY;
                }
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int phyiscalDamage, ref int magicalDamage, ref int trueDamage, ref bool crit) {
            var modProj = proj.ModProjectile;
            RoseThorn thorn = modProj as RoseThorn;
            if (thorn != null) {
                //StatusEffectManager.AddEffect(target, new ThornPoison(THORN_POISON_DAMAGE, THORN_POISON_HEALMOD, THORN_POISON_DURATION, true, User.whoAmI));
            }
        }
    }
}