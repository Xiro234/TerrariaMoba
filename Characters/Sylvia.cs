using TerrariaMoba;
using TerrariaMoba.Buffs;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariaMoba.Players;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using System;
using System.Collections.Generic;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Enums;
using TerrariaMoba.Stats;
using EnsnaringVines = TerrariaMoba.Abilities.Sylvia.EnsnaringVines;
using VerdantFury = TerrariaMoba.Abilities.Sylvia.VerdantFury;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        private bool IsPhasing = false;
        private bool SylviaUlt1 = false;
        private int SylviaUlt1Timer = 0;
        private int NumberJavelins = 0;
        public float VerdantFuryBuff = 1.25f;
        private int VerdantFuryTime = 180;
        public float VerdantFuryIncrease = 0.05f;
        public int JunglesWrathTime = 180;

        public Sylvia(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Sylvia;
        }

        public override void ChooseCharacter() {
                Main.NewText("Sylvia");
                var plr = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
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

                EnsnaringVines abilityOne = new EnsnaringVines(player);
                abilities[0] = abilityOne;
                
                VerdantFury abilityTwo = new VerdantFury(player);
                abilities[1] = abilityTwo;
                
                /*
                UltimateName = "Flourish";
                UltimateCooldown = 60 * 60;
                UltimateIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaUltimateOne");
                
                TraitName = "Jungle's Wrath";
                TraitCooldown = 0;
                TraitIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaTrait");
                */
                
                CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaIcon");

                VerdantFuryTime = 20;
                //TalentSelect();
        }
/*
        public override void AbilityOneOnCast(Player player) {
            if (player == Main.LocalPlayer) {
                
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
            
            if (player == Main.LocalPlayer) {
                Vector2 position = player.Top;
                Vector2 playerToMouse = Main.MouseWorld - player.Center;
                int direction = -Math.Sign((int) playerToMouse.X);

                Vector2 velocity = new Vector2(direction * 0.5f, -0.866f); //Unit vector in specific direction
                velocity *= 12;

                Projectile.NewProjectile(position, velocity, TerrariaMoba.Instance.ProjectileType("SylviaUlt1Teleport"),
                    0, 0, player.whoAmI);
            }
            IsPhasing = true;
            UltimateCooldownTimer = UltimateCooldown;
        }
        
        public override void UltimateInUse(Player player) { }
        public override void UltimateOnEnd(Player player) { }
        */
        public override void ReadCharacter(BinaryReader reader) {
            base.ReadCharacter(reader);
        }

        public override void WriteCharacter(BinaryWriter writer) {
            base.WriteCharacter(writer);
        }

        public override void PreUpdate(Player player) {
            if (SylviaUlt1Timer > 0) {
                SylviaUlt1Timer--;
                if (SylviaUlt1Timer == 0) {
                    SylviaUlt1 = false;
                    NumberJavelins = 0;
                    //SyncSylviaUlt1Packet.Write(player.whoAmI, SylviaUlt1);
                }
            }
        }

        public override void PostUpdateBuffs(Player player) {
            if (IsPhasing) {
                player.immune = true;
                player.immuneTime = 1;
            }
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack, Player player) {
            if (player.GetModPlayer<MobaPlayer>().VerdantFury && item.type == TerrariaMoba.Instance.ItemType("SylviaBow")) {
                speedX *= VerdantFuryIncrease;
                speedY *= VerdantFuryIncrease;
            }
                
            //Flourish
            if (NumberJavelins > 0) {
                Vector2 velocity = new Vector2();
                velocity.X = speedX;
                velocity.Y = speedY;
                velocity.Normalize();
                velocity *= 15;

                Projectile.NewProjectile(position.X, position.Y, velocity.X, velocity.Y,
                    TerrariaMoba.Instance.ProjectileType("SylviaUlt1Projectile"), 40, knockBack, player.whoAmI);
                NumberJavelins--;
                if (NumberJavelins == 0) {
                    SylviaUlt1 = false;
                    //SyncSylviaUlt1Packet.Write(player.whoAmI, SylviaUlt1);
                }
                    
                return false;
            }

            return true;
        }

        public override float UseTimeMultiplier(Item item, Player player) {
            if (player.GetModPlayer<MobaPlayer>().VerdantFury && item.type == TerrariaMoba.Instance.ItemType("SylviaBow")) {
                return VerdantFuryIncrease;
            }

            return 1f;
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers, Player player) {
            if (IsPhasing) {
                foreach (PlayerLayer layer in layers) {
                    layer.visible = false;
                }
            }
        }

        public override void PreUpdateMovement(Player player) {
            if (SylviaUlt1) {
                if (player.velocity.Y != 0f) { //Ripped from webbed
                    player.velocity = new Vector2(0f, 1E-06f);
                }
                else {
                    player.velocity = Vector2.Zero;
                }

                player.gravity = 0f;
                player.moveSpeed = 0f;
            }
        }

        public override void PostUpdateRunSpeeds(Player player) {
            MobaPlayer modPlayer = player.GetModPlayer<MobaPlayer>();
            float moveSpeedAdd = 1f;
        }
        
        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit, Player player) {
            if (proj.ranged) {
                target.AddBuff(BuffType<Buffs.JunglesWrath>(), 240, false);
            }
        }

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