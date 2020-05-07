using TerrariaMoba;
using TerrariaMoba.Buffs;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariaMoba.Players;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using System;
using TerrariaMoba.Utils;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        private int VerdantFuryTime;

        public Sylvia() {
            Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().IsSylvia = true;
            
            AbilityOneName = "Enrapturing Vines";
            AbilityOneCooldown = SylviaUtils.GetAbilityOneBaseCd();

            AbilityTwoName = "Verdant Fury";
            AbilityTwoCooldown = SylviaUtils.GetAbilityTwoBaseCd();

            VerdantFuryTime = SylviaUtils.GetVerdantFuryBaseTime();
        }

        public override void AbilityOne() {
            Vector2 position = Main.LocalPlayer.Center;
            Vector2 playerToMouse = Main.MouseWorld - Main.LocalPlayer.Center;
            
            int direction = Math.Sign((int)playerToMouse.X);

            Vector2 velocity = new Vector2(direction * 6, 0);

            Projectile.NewProjectile(position, velocity, TerrariaMoba.Instance.ProjectileType("EnrapturingVinesSpawner"), 30, 0, Main.LocalPlayer.whoAmI);
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
            Main.LocalPlayer.AddBuff(BuffType<Buffs.VerdantFury>(), VerdantFuryTime);
            AbilityTwoCooldownTimer = AbilityTwoCooldown;
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