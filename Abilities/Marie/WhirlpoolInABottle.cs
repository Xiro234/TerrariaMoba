using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Marie {
    public class WhirlpoolInABottle : Ability {
        public WhirlpoolInABottle(Player myPlayer) : base(myPlayer) {
            Name = "Whirlpool in a Bottle";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieAbilityOne");
        }
        
        public override void Cast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                Vector2 position = player.Center;
                Vector2 playerToMouse = Main.MouseWorld - position;
                double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                float dirX = (float)(playerToMouse.X * (12.0 / mag));
                float dirY = (float)(playerToMouse.Y * (12.0 / mag));
                /*
                if (player.direction < 0 && dirX > 0 || player.direction > 0 && dirX < 0) {
                    dirX *= -1;
                }
                */
                Vector2 velocity = new Vector2(dirX, dirY);

                Projectile.NewProjectile(position, velocity, 
                    TerrariaMoba.Instance.ProjectileType("WBBottle"), 120, 0, player.whoAmI);
                Main.PlaySound(SoundID.Item1, player.Center);
            }
            
            Cooldown = 10 * 60;
        }
    }
}