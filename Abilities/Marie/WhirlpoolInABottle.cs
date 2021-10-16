using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Marie;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Marie {
    public class WhirlpoolInABottle : Ability, IModifyHitPvpWithProj {
        public WhirlpoolInABottle() : base("Whirlpool in a Bottle", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieAbilityOne").Value; }

        public const int BOTTLE_BASE_DAMAGE = 500;
        public const int POOL_BASE_DAMAGE = 200;
        public const int POOL_BASE_DURATION = 150;
        public const int SLOW_BASE_DURATION = 90;
        
        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - User.Center;
                double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                float dirX = (float)(playerToMouse.X * (12.0 / mag));
                float dirY = (float)(playerToMouse.Y * (12.0 / mag));
                
                if (User.direction < 0 && dirX > 0 || User.direction > 0 && dirX < 0) {
                    dirX *= -1;
                }
                Vector2 velocity = new Vector2(dirX, dirY);

                Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this),User.Center, velocity, 
                    ModContent.ProjectileType<WBBottle>(), BOTTLE_BASE_DAMAGE, 0, User.whoAmI);
                SoundEngine.PlaySound(SoundID.Item1, User.Center);
                
                WBBottle bottle = proj.ModProjectile as WBBottle;
                
                if (bottle != null) {
                    bottle.PoolDamage = POOL_BASE_DAMAGE;
                    bottle.PoolDuration = POOL_BASE_DURATION;
                }
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProj = proj.ModProjectile;
            WBWhirlpool pool = modProj as WBWhirlpool;
            if (pool != null) {
                StatusEffectManager.AddEffect(target, new EnsnaringVinesEffect(SLOW_BASE_DURATION, true));
            }
        }
    }
}