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
using TerrariaMoba.Players;
using System.Collections.Generic;

namespace TerrariaMoba.Abilities.Flibnob {
    public class FlameBelch : Ability, IModifyHitPvpWithProj {
        public FlameBelch(Player player) : base(player, "Flame Belch", 30, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobAbilityOne").Value; }

        public const int FLAME_BASE_DAMAGE = 100;
        public const int FLAME_BASE_DELAY = 75;
        public const int BURN_BASE_DAMAGE = 5;
        public const int MELT_BASE_DAMAGE = 25;
        public const int BURN_BASE_DURATION = 180;
        public const int MANA_DRAIN = 50;
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

        public override void WhileActive() {
            if (timer > 0) {
                timer--;
            }

            if (timer == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    var mobaPlayer = User.GetModPlayer<MobaPlayer>();
                    if (mobaPlayer.CurrentResource < MANA_DRAIN) {
                        IsActive = false;
                    } else {
                        Vector2 playerToMouse = Main.MouseWorld - User.Center;
                        double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                        float dirX = (float)(playerToMouse.X * (6.0 / mag));
                        float dirY = (float)(playerToMouse.Y * (6.0 / mag));
                        Vector2 vel = new Vector2(dirX, dirY);

                        SoundEngine.PlaySound(SoundID.DD2_OgreAttack, User.Center);
                        Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), User.Center,
                            vel, ModContent.ProjectileType<FlameBelchSpawner>(), 1, 0, User.whoAmI);
                        TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: FLAME_BASE_DAMAGE);

                        mobaPlayer.CurrentResource -= MANA_DRAIN;
                    }
                }
                timer = FLAME_BASE_DELAY;
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProjectile = proj.ModProjectile;
            FlameBelchSpawner flame = modProjectile as FlameBelchSpawner;

            if (flame != null) {
                var mobaPlayer = target.GetModPlayer<MobaPlayer>();

                foreach (var effect in new List<StatusEffect>(mobaPlayer.EffectList)) {
                    if (effect is FlameBelchEffect) {
                        StatusEffectManager.RemoveEffect(target, effect);
                        StatusEffectManager.AddEffect(target, new FlameBelchSecondEffect(MELT_BASE_DAMAGE, BURN_BASE_DURATION, true, User.whoAmI));
                        return;
                    } else if (effect is FlameBelchSecondEffect) {
                        effect.ReApply();
                        return;
                    } else {
                        Logging.PublicLogger.Debug("FlameBelch.cs: This shouldn't happen.");
                        break;
                    }
                }

                StatusEffectManager.AddEffect(target, new FlameBelchEffect(BURN_BASE_DAMAGE, BURN_BASE_DURATION, true, User.whoAmI));
            }
        }
    }
}