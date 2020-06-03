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
using TerrariaMoba.Enums;
using TerrariaMoba.Stats;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        private int VerdantFuryTime;

        public Sylvia(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Sylvia;
        }

        public override void ChooseCharacter() {
                Main.NewText("Sylvia");
                var plr = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
                var sylviaPlayer = Main.LocalPlayer.GetModPlayer<SylviaPlayer>();
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
                Main.LocalPlayer.Male = false;
                Main.LocalPlayer.hair = 55;
                Main.LocalPlayer.hairColor = new Color(52, 133, 34);
                Main.LocalPlayer.skinColor = new Color(198,134,66);
                Main.LocalPlayer.eyeColor = new Color(84,42,14);
                plr.customStats.maxHealth = 2000;
                Main.LocalPlayer.statLifeMax2 = 2000;
                Main.LocalPlayer.statLife = 2000;
                AbilityOneName = "Ensnaring Vines";
                AbilityOneCooldown = 30 * 60;
                AbilityOneIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaAbilityOne");

                AbilityTwoName = "Verdant Fury";
                AbilityTwoCooldown = 10 * 60;
                AbilityTwoIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaAbilityTwo");

                UltimateName = "Flourish";
                UltimateCooldown = 60 * 60;
                UltimateIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaUltimateOne");
                
                TraitName = "Jungle's Wrath";
                TraitCooldown = 0;
                TraitIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaTrait");
                
                CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaIcon");

                VerdantFuryTime = sylviaPlayer.MySylviaStats.GetVerdantFuryTime();
                //TalentSelect();
        }

        public override void AbilityOneOnCast(Player player) {
            if (player == Main.LocalPlayer) {
                Vector2 position = player.Center;
                Vector2 playerToMouse = Main.MouseWorld - player.Center;

                int direction = Math.Sign((int) playerToMouse.X);
                Vector2 velocity = new Vector2(direction * 6, 0);

                Projectile.NewProjectile(position, velocity,
                    TerrariaMoba.Instance.ProjectileType("EnsnaringVinesSpawner"), 30, 0, player.whoAmI);
            }

            AbilityOneCooldownTimer = AbilityOneCooldown;
        }

        public override void AbilityOneInUse(Player player) { }
        public override void AbilityOneOnEnd(Player player) { }

        public override void AbilityTwoOnCast(Player player) {
            player.AddBuff(BuffType<Buffs.VerdantFury>(), VerdantFuryTime);
            AbilityTwoCooldownTimer = AbilityTwoCooldown;
        }
        
        public override void AbilityTwoInUse(Player player) { }
        public override void AbilityTwoOnEnd(Player player) { }

        public override void UltimateOnCast(Player player) {
            /*
            Vector2 position = Main.LocalPlayer.Center;
            Vector2 playerToMouse = Main.MouseWorld - Main.LocalPlayer.Center;
            playerToMouse.Normalize();

            Vector2 velocity = playerToMouse *= 10;
            
            Projectile.NewProjectile(position, velocity, TerrariaMoba.Instance.ProjectileType("SylviaUlt2"), 30, 0, Main.LocalPlayer.whoAmI);
            */
            if (player == Main.LocalPlayer) {
                Vector2 position = player.Top;
                Vector2 playerToMouse = Main.MouseWorld - player.Center;
                int direction = -Math.Sign((int) playerToMouse.X);

                Vector2 velocity = new Vector2(direction * 0.5f, -0.866f); //Unit vector in specific direction
                velocity *= 12;

                Projectile.NewProjectile(position, velocity, TerrariaMoba.Instance.ProjectileType("SylviaUlt1Teleport"),
                    0, 0, player.whoAmI);
            }
            player.GetModPlayer<SylviaPlayer>().IsPhasing = true;
            UltimateCooldownTimer = UltimateCooldown;
        }
        
        public override void UltimateInUse(Player player) { }
        public override void UltimateOnEnd(Player player) { }

        public override void LevelUp() {
            level += 1;
            TalentSelect();
        }

        public override void TalentSelect() {
            switch (level) {
                case 1:
                    Main.NewText("Move speed!");
                    Main.NewText("Leap!");
                    Main.NewText("Defense!");
                    canSelectTalent = true;
                    break;
                case 4:
                    Main.NewText("Move speed!");
                    Main.NewText("Leap!");
                    Main.NewText("Defense!");
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