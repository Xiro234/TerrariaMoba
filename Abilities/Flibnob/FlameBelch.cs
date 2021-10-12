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
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Flibnob {
    public class FlameBelch : Ability, IModifyHitPvpWithProj {
        public FlameBelch() : base("Flame Belch", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Flibnob/FlibnobAbilityOne").Value; }

        public const int FLAME_BASE_DAMAGE = 225;
        public const int FLAME_BASE_NUMBER = 4;
        public const int FLAME_BASE_DELAY = 60;
        
        public const int BURN_BASE_DURATION = 150;
        
        public int timer;
        public int remainingFlames;

        public override void OnCast() {
            timer = FLAME_BASE_DELAY;
            remainingFlames = FLAME_BASE_NUMBER;
            IsActive = true;
        }

        public override void WhileActive() {
            timer--;
            if (timer == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Vector2 playerToMouse = Main.MouseWorld - User.Center;
                    double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                    float dirX = (float)(playerToMouse.X * (6.0 / mag));
                    float dirY = (float)(playerToMouse.Y * (6.0 / mag));
                    Vector2 vel = new Vector2(dirX, dirY);
                
                    SoundEngine.PlaySound(SoundID.DD2_OgreAttack, User.Center);
                    Projectile.NewProjectile(new ProjectileSource_Ability(User, this), User.Center, vel,
                        ModContent.ProjectileType<FlameBelchSpawner>(), FLAME_BASE_DAMAGE, 0, User.whoAmI);
                }
                remainingFlames--;
            }
            
            if (remainingFlames == 0) {
                TimeOut();
            }
        }
        
        public override void TimeOut() {
            timer = 0;
            remainingFlames = 0;
            IsActive = false;
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            //TODO - Add burning effect.
            var ModProjectile = proj.ModProjectile;
            FlameBelchSpawner trap = ModProjectile as FlameBelchSpawner;
            if (trap != null) {
                StatusEffectManager.AddEffect(target, new EnsnaringVinesEffect(BURN_BASE_DURATION, true));
            }
        }
    }
}