using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Flibnob;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class FlameBelch : Ability, IModifyHitPvpWithProj {
        public FlameBelch(Player player) : base(player, "Flame Belch", 30, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobAbilityOne").Value; }

        public const int FLAME_BASE_DAMAGE = 225;
        public const int FLAME_BASE_DELAY = 60;
        public const int BURN_BASE_DURATION = 120;
        public int timer;

        public override void OnCast() {
            if (IsActive) {
                IsActive = false;
                timer = 0;
            } else {
                timer = FLAME_BASE_DELAY;
                IsActive = true;
            }
        }

        //TODO - Make into toggle that drains mana each flame cast
        public override void WhileActive() {
            if (timer > 0) {
                timer--;
            }

            if (timer == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Vector2 playerToMouse = Main.MouseWorld - User.Center;
                    double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                    float dirX = (float)(playerToMouse.X * (6.0 / mag));
                    float dirY = (float)(playerToMouse.Y * (6.0 / mag));
                    Vector2 vel = new Vector2(dirX, dirY);
                
                    SoundEngine.PlaySound(SoundID.DD2_OgreAttack, User.Center);
                    Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), User.Center, 
                        vel, ModContent.ProjectileType<FlameBelchSpawner>(), 1, 0, 
                        User.whoAmI);
                    TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: FLAME_BASE_DAMAGE);
                }
                //player resource reduced by 50
                timer = FLAME_BASE_DELAY;
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var ModProjectile = proj.ModProjectile;
            FlameBelchSpawner flame = ModProjectile as FlameBelchSpawner;
            if (flame != null) {
                if (StatusEffectManager.PlayerHasEffectType<FlameBelchSecondEffect>(target)) {
                    StatusEffectManager.AddEffect(target, new FlameBelchSecondEffect(User.whoAmI, BURN_BASE_DURATION, true));
                } else if (StatusEffectManager.PlayerHasEffectType<FlameBelchEffect>(target)) {
                    StatusEffectManager.RemoveEffect(target, StatusEffectManager.GetIDOfEffect(new FlameBelchEffect(User.whoAmI, BURN_BASE_DURATION, true)));
                    StatusEffectManager.AddEffect(target, new FlameBelchSecondEffect(User.whoAmI, BURN_BASE_DURATION, true));
                } else {
                    StatusEffectManager.AddEffect(target, new FlameBelchEffect(User.whoAmI, BURN_BASE_DURATION, true));
                }
            }
        }
    }
}