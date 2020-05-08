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
using TerrariaMoba.Stats;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        private int VerdantFuryTime;

        public Sylvia() {
            Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().CharacterName = "sylvia";
            Item vanityHelm = new Item();
            vanityHelm.SetDefaults(208);
            Item vanityChest = new Item();
            vanityChest.SetDefaults(1853);
            Item vanityLeg = new Item();
            vanityLeg.SetDefaults(1854);
            Item primary = new Item();
            primary.SetDefaults(TerrariaMoba.Instance.ItemType("SylviaBow"));

            Main.LocalPlayer.armor[10] = vanityHelm;
            Main.LocalPlayer.armor[11] = vanityChest;
            Main.LocalPlayer.armor[12] = vanityLeg;

            Main.LocalPlayer.inventory[0] = primary;

            AbilityOneName = "Enrapturing Vines";
            AbilityOneCooldown = SylviaStats.GetAbilityOneBaseCd();

            AbilityTwoName = "Verdant Fury";
            AbilityTwoCooldown = SylviaStats.GetAbilityTwoBaseCd();

            VerdantFuryTime = SylviaStats.GetVerdantFuryBaseTime();
        }

        public override void AbilityOne() {
            Vector2 position = Main.LocalPlayer.Center;
            Vector2 playerToMouse = Main.MouseWorld - Main.LocalPlayer.Center;
            
            int direction = Math.Sign((int)playerToMouse.X);
            Vector2 velocity = new Vector2(direction * 6, 0);

            Projectile.NewProjectile(position, velocity, TerrariaMoba.Instance.ProjectileType("EnrapturingVinesSpawner"), 30, 0, Main.LocalPlayer.whoAmI);
        }

        public override void AbilityTwo() {
            Main.LocalPlayer.AddBuff(BuffType<Buffs.VerdantFury>(), VerdantFuryTime);
            AbilityTwoCooldownTimer = AbilityTwoCooldown;
        }

        public override void Ultimate() {
            /*
            Vector2 position = Main.LocalPlayer.Center;
            Vector2 playerToMouse = Main.MouseWorld - Main.LocalPlayer.Center;
            playerToMouse.Normalize();

            Vector2 velocity = playerToMouse *= 10;
            
            Projectile.NewProjectile(position, velocity, TerrariaMoba.Instance.ProjectileType("SylviaUlt2"), 30, 0, Main.LocalPlayer.whoAmI);
            */

            Vector2 position = Main.LocalPlayer.Top;
            Vector2 playerToMouse = Main.MouseWorld - Main.LocalPlayer.Center;
            int direction = -Math.Sign((int)playerToMouse.X);

            Vector2 velocity = new Vector2(direction * 0.5f ,-0.866f); //Unit vector in specific direction
            velocity *= 12;

            Projectile.NewProjectile(position, velocity, TerrariaMoba.Instance.ProjectileType("SylviaUlt1Teleport"), 0, 0, Main.LocalPlayer.whoAmI);

            Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().IsPhasing = true;
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