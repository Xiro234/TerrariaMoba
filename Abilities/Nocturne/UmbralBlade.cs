using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles;

namespace TerrariaMoba.Abilities.Nocturne {
    public class UmbralBlade : Ability {
        public UmbralBlade() : base("Umbral Blade", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public const int BLADE_BASE_DAMAGE = 500;
        public const int VISION_BASE_DURATION = 60;
        public const int TIMESTOP_BASE_DURATION = 120;
        
        //TODO - Fix hitbox
        //TODO - Inflict blackout and time dilation effects

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - User.Center;
                double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                float dirX = (float)(playerToMouse.X * (12.0 / mag));
                float dirY = (float)(playerToMouse.Y * (7.0 / mag));
                
                if (User.direction < 0 && dirX > 0 || User.direction > 0 && dirX < 0) {
                    dirX *= -1;
                }
                Vector2 velocity = new Vector2(dirX, dirY);
                
                Projectile.NewProjectile(new ProjectileSource_Ability(User, this), User.Center, velocity, 
                    ModContent.ProjectileType<Projectiles.Nocturne.UmbralBlade>(), BLADE_BASE_DAMAGE, 0, User.whoAmI);
            }
        }
    }
}