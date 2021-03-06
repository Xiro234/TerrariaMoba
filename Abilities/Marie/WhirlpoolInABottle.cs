/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Marie {
    [Serializable]
    public class WhirlpoolInABottle : Ability {
        public WhirlpoolInABottle(Player myPlayer) : base(myPlayer) {
            Name = "Whirlpool in a Bottle";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieAbilityOne");
        }
        
        public override void Cast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 position = User.Center;
                Vector2 playerToMouse = Main.MouseWorld - position;
                double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                float dirX = (float)(playerToMouse.X * (12.0 / mag));
                float dirY = (float)(playerToMouse.Y * (12.0 / mag));
                /*
                if (player.direction < 0 && dirX > 0 || player.direction > 0 && dirX < 0) {
                    dirX *= -1;
                }
                #1#
                Vector2 velocity = new Vector2(dirX, dirY);

                Projectile.NewProjectile(position, velocity, 
                    TerrariaMoba.Instance.ProjectileType("WBBottle"), (int)User.GetModPlayer<MobaPlayer>().MarieStats.A1BottleDmg.Value, 0, User.whoAmI);
                Main.PlaySound(SoundID.Item1, User.Center);
            }
            
            cooldownTimer = 10 * 60;
        }
    }
}*/