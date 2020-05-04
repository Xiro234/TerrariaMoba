using TerrariaMoba;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader.Config.UI;
using System;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        public override void AbilityOne(Player player) {
            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 10f;
            Projectile.NewProjectile(player.Center, velocity, 1, 10, 2, Main.myPlayer);
            player.direction = Math.Sign(velocity.X);
        }

        public override void AbilityOneAnimation(Player player, ref int animCounter) {
            if (animCounter == -1) {
                animCounter = 10;
            }
            
            if (animCounter >= 5) {
                player.bodyFrame.Y = 1 * 56;
            }
            else if(animCounter > 0) {
                player.bodyFrame.Y = 2 * 56;
            }
            animCounter--;
        }

        public override void AbilityTwo(Player player) {
            
        }

        public override void LevelUp() {
            Main.NewText("I leveled up!!!");
        }
    }
}