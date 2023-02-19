using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Marie;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Marie;

namespace TerrariaMoba.Abilities.Marie {
    public class EyeOfTheStorm : Ability {
        public EyeOfTheStorm(Player player) : base(player, "Eye of the Storm", 180, 50, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieUltimateTwo").Value; }

        public const int LIGHTNING_DAMAGE = 300;
        public const float LIGHTNING_SPEED = 3f;
        public const int RAIN_DAMAGE = 50;
        public const float RAIN_SPEED = 4f;
        public const int CLOUD_DAMAGE = 333;
        public const int CLOUD_DURATION = 380;
        
        public const float SHOCK_SLOW_MAG = -0.25f;
        public const int SHOCK_MR_MODIFIER = -10;
        public const int SHOCK_DURATION = 120;
        
        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - User.Center;
                double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                float dirX = (float)(playerToMouse.X * (5.0 / mag));
                float dirY = (float)(playerToMouse.Y * (5.0 / mag));
                if (User.direction < 0 && dirX > 0 || User.direction > 0 && dirX < 0) {
                    dirX *= -1;
                }
                if (dirY > -3f) {
                    dirY = -3f;
                }
                Vector2 velocity = new Vector2(dirX, dirY);

                Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this),
                    User.Center, velocity, ModContent.ProjectileType<ESSpawner>(), 1, 0, User.whoAmI);
                SoundEngine.PlaySound(SoundID.Item66, User.Center);

                ESSpawner eye = proj.ModProjectile as ESSpawner;
                
                if (eye != null) {
                    eye.LightningDamage = LIGHTNING_DAMAGE;
                    eye.LightningSpeed = LIGHTNING_SPEED;
                    eye.RainDamage = RAIN_DAMAGE;
                    eye.RainSpeed = RAIN_SPEED;
                    eye.CloudDamage = CLOUD_DAMAGE;
                    eye.CloudDuration = CLOUD_DURATION;
                }
            }
        }
        
        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProj = proj.ModProjectile;
            
            ESLightning lightning = modProj as ESLightning;
            if (lightning != null) {
                StatusEffectManager.AddEffect(target, new StormShockEffect(SHOCK_SLOW_MAG, SHOCK_MR_MODIFIER, SHOCK_DURATION, true, User.whoAmI));
            }
        }
    }
}