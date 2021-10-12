using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles.Flibnob;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Flibnob {
    public class FlameBelch : Ability, IModifyHitPvpWithProj {
        public FlameBelch() : base("Flame Belch", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobAbilityOne"); }

        public const int FIREBALL_BASE_DAMAGE = 216;
        public const int FIREBALL_BASE_NUMBER = 4;
        public const int FIREBALL_BASE_DELAY = 75;
        public const int FIREBALL_BASE_DURATION = 120;
        
        public const int BURN_BASE_DURATION = 120;
        
        private int Timer;
        private int RemainingFlames;

        public override void OnCast() {
            Timer = FIREBALL_BASE_DELAY;
            RemainingFlames = FIREBALL_BASE_NUMBER;
            IsActive = true;
        }

        public override void WhileActive() {
            Timer--;
            if (Timer == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Vector2 playerToMouse = Main.MouseWorld - User.Center;
                    double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                    float dirX = (float)(playerToMouse.X * (7.0 / mag));
                    float dirY = (float)(playerToMouse.Y * (7.0 / mag));
                    Vector2 vel = new Vector2(dirX, dirY);
                
                    Main.PlaySound(SoundID.DD2_OgreAttack, User.Center);
                    Projectile.NewProjectile(User.Center, vel,
                        TerrariaMoba.Instance.ProjectileType("FlameBelchProj"), FIREBALL_BASE_DAMAGE, 0, User.whoAmI);
                }
                RemainingFlames--;
                Timer = FIREBALL_BASE_DELAY;
            }
            
            if (RemainingFlames == 0) {
                TimeOut();
            }
        }
        
        public override void TimeOut() {
            Timer = 0;
            RemainingFlames = 0;
            IsActive = false;
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProjectile = proj.modProjectile;
            FlameBelchProj fireball = modProjectile as FlameBelchProj;
            if (fireball != null) {
                //TODO - Add burning effect.
                StatusEffectManager.AddEffect(target, new EnsnaringVinesEffect(BURN_BASE_DURATION, true));
            }
        }
    }
}