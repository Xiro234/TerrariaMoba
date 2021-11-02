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

namespace TerrariaMoba.Abilities.Marie {
    public class EyeOfTheStorm : Ability {
        public EyeOfTheStorm() : base("Eye of the Storm", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieAbilityTwo").Value; }

        public const int LIGHTNING_DAMAGE = 569;
        public const float LIGHTNING_SPEED = 3.25f;
        public const int SHOCK_DURATION = 120;
        
        public const int RAIN_DAMAGE = 73;
        public const float RAIN_SPEED = 4.5f;
        public const int WET_DURATION = 120;
        
        public const int CLOUD_DAMAGE = 250;
        public const int CLOUD_DURATION = 300;

        public const int EYE_DAMAGE = 500;
        
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

                Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this),User.Center, velocity, ModContent.ProjectileType<ESSpawner>(), 
                    0, 0, User.whoAmI);
                SoundEngine.PlaySound(SoundID.Item66, User.Center);

                if (proj != null) {
                    
                }
            }
        }
    }
}