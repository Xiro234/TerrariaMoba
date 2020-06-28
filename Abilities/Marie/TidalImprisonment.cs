using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Marie {
    public class TidalImprisonment : Ability {
        public TidalImprisonment(Player myPlayer) : base(myPlayer) {
            Name = "Tidal Imprisonment";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieUltimateTwo");
        }
        
        public override void Cast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - player.Center;
                double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                float dirX = (float)(playerToMouse.X * (9.0 / mag));
                float dirY = (float)(playerToMouse.Y * (9.0 / mag));
                if (player.direction < 0 && dirX > 0 || player.direction > 0 && dirX < 0) {
                    dirX *= -1;
                }
                Vector2 velocity = new Vector2(dirX, dirY);

                Projectile.NewProjectile(player.Top, velocity,
                    TerrariaMoba.Instance.ProjectileType("WaterShackleBomb"), 30, 0, player.whoAmI);
            }
            
            Cooldown = 10 * 60;
        }
    }
}