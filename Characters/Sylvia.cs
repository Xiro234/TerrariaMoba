using TerrariaMoba;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariaMoba.Players;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using System;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        public override void AbilityOne() {
            Player player = Main.LocalPlayer;
            Vector2 velocity = Main.MouseWorld - player.Center;
            Vector2 position = player.Center;
            velocity.Normalize();
            velocity *= 10f;
            
            if (talentArray[0, 0]) {
                int numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(45);
                position += Vector2.Normalize(velocity) * 45f;
                
                for (int i = 0; i < numberProjectiles; i++) {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    Projectile.NewProjectile(position, perturbedSpeed, 1, 10, 2, Main.myPlayer);
                }
            }
            else if (talentArray[0, 1]) {
                Projectile.NewProjectile(position, velocity * 2f, 1, 10, 2, Main.myPlayer);
            }
            else if (talentArray[0, 2]) {
                Projectile.NewProjectile(position, velocity, 2, 10, 2, Main.myPlayer);
            }
            else {
                Projectile.NewProjectile(position, velocity, 1, 10, 2, Main.myPlayer);
            }
            
        }

        public override void AbilityOneAnimation(ref int animCounter) {
            Player player = Main.LocalPlayer;
            
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

        public override void AbilityTwo() {
            
        }

        public override void LevelUp() {
            level += 1;
            TalentSelect();
        }

        public override void TalentSelect() {
            switch (level) {
                case 1:
                    break;
                case 4:
                    Main.NewText("Shoot 3 Arrows!");
                    Main.NewText("Shoot faster arrows!");
                    Main.NewText("Shoot Flaming Arrows!");
                    canSelectTalent = true;
                    break;
                case 7:
                    break;
                case 10:
                    break;
                case 13:
                    break;
                case 16:
                    break;
                case 20:
                    break;
            }
        }

        
    }
}