using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace TerrariaMoba.Abilities.Marie {
    public class TidalImprisonment : Ability {
        public TidalImprisonment(Player myPlayer) : base(myPlayer) {
            Name = "Tidal Imprisonment";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieUltimateTwo");
        }
        
        public override void Cast() {
            Vector2 position = Main.LocalPlayer.Top;
            Vector2 playerToMouse = Main.MouseWorld - Main.LocalPlayer.Center;
            int velX = Math.Sign((int)playerToMouse.X);
            int velY = (int) MathHelper.Clamp((playerToMouse.Y / 16.0f), -10f, 10f);
            Vector2 velocity = new Vector2(velX * 5, velY);

            Projectile proj = Main.projectile[Projectile.NewProjectile(position, velocity, 
                TerrariaMoba.Instance.ProjectileType("WaterShackleBomb"), 30, 0, Main.LocalPlayer.whoAmI)];
            Cooldown = 10 * 60;
        }
    }
}