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
using Terraria.DataStructures;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        private bool IsPhasing = false;
        private bool SylviaUlt1 = false;
        private int SylviaUlt1Timer = 0;
        public float VerdantFuryBuff = 1.25f;
        private int VerdantFuryTime = 180;
        public float VerdantFuryIncrease = 0.05f;
        public int JunglesWrathTime = 180;

        public Sylvia(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Sylvia;
        }

        public override void ChooseCharacter() {
                Main.NewText("Sylvia");
                var mobaPlayer = player.GetModPlayer<MobaPlayer>();
                Item vanityHelm = new Item();
                vanityHelm.SetDefaults(208);
                Item vanityChest = new Item();
                vanityChest.SetDefaults(1853);
                Item vanityLeg = new Item();
                vanityLeg.SetDefaults(1854);
                Item primary = new Item();
                primary.SetDefaults(TerrariaMoba.Instance.ItemType("SylviaBow"));

                player.armor[10] = vanityHelm;
                player.armor[11] = vanityChest;
                player.armor[12] = vanityLeg;
                player.inventory[0] = primary;
                player.Male = false;
                player.hair = 55;
                player.hairColor = new Color(52, 133, 34);
                player.skinColor = new Color(198,134,66);
                player.eyeColor = new Color(84,42,14);
                baseMaxHealth = 2000;
                player.statLifeMax2 = baseMaxHealth;
                player.statLife = 2000;

                EnsnaringVines abilityOne = new EnsnaringVines(player);
                abilities[0] = abilityOne;
                
                VerdantFury abilityTwo = new VerdantFury(player);
                abilities[1] = abilityTwo;
                
                
                Flourish ultimate = new Flourish(player);
                abilities[2] = ultimate;
                
                
                /*
                PlanterasLastWill ultimate = new PlanterasLastWill(player);
                abilities[2] = ultimate;
                */
                
                JunglesWrath trait = new JunglesWrath(player);
                abilities[3] = trait;

                CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaIcon");

                VerdantFuryTime = 20;
                //TalentSelect();
        }

        public override void ReadCharacter(BinaryReader reader) {
            base.ReadCharacter(reader);
        }

        public override void WriteCharacter(BinaryWriter writer) {
            base.WriteCharacter(writer);
        }

        public override void PreUpdate() {
        }

        public override void PostUpdateBuffs() {

        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {
            if (player.GetModPlayer<MobaPlayer>().SylviaEffects.VerdantFury && item.type == TerrariaMoba.Instance.ItemType("SylviaBow")) {
                speedX *= VerdantFuryIncrease;
                speedY *= VerdantFuryIncrease;
            }
            
            foreach (Ability ability in abilities) {
                var flourish = ability as Flourish;
                if (flourish != null) {
                    if (flourish.IsActive && flourish.NumberJavelins > 0) {
                        if (flourish.NumberJavelins > 0) {
                            Vector2 velocity = new Vector2();
                            velocity.X = speedX;
                            velocity.Y = speedY;
                            velocity.Normalize();
                            velocity *= 15;

                            Projectile.NewProjectile(position.X, position.Y, velocity.X, velocity.Y,
                                TerrariaMoba.Instance.ProjectileType("SylviaUlt1Projectile"), 40, knockBack, player.whoAmI);
                            flourish.NumberJavelins--;

                            return false;
                        }
                    }
                }
            }
            
            return true;
        }

        public override float UseTimeMultiplier(Item item) {
            if (player.GetModPlayer<MobaPlayer>().SylviaEffects.VerdantFury && item.type == TerrariaMoba.Instance.ItemType("SylviaBow")) {
                return VerdantFuryIncrease;
            }

            return 1f;
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            foreach (Ability ability in abilities) {
                var flourish = ability as Flourish;
                if (flourish != null) {
                    if (flourish.teleporting) {
                        foreach (PlayerLayer layer in layers) {
                            layer.visible = false;
                        }
                    }
                }
            }
        }
        
        public override void PostUpdateRunSpeeds() {
            var modPlayer = player.GetModPlayer<MobaPlayer>();
            float moveSpeedAdd = 1f;
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            if (proj.type == TerrariaMoba.Instance.ProjectileType("SylviaArrow")) {
                var otherMobaPlayer = target.GetModPlayer<MobaPlayer>();
                bool doEffects = false;

                if (!otherMobaPlayer.SylviaEffects.JunglesWrath) {
                    otherMobaPlayer.SylviaEffects.JunglesWrathCount = 1;
                }
                else {
                    if (++otherMobaPlayer.SylviaEffects.JunglesWrathCount >= 5) {
                        otherMobaPlayer.SylviaEffects.JunglesWrathCount -= 5;
                        damage += 50;
                        doEffects = true;
                    }
                }
                Packets.JunglesWrathAddPacket.Write(otherMobaPlayer.player.whoAmI, otherMobaPlayer.SylviaEffects.JunglesWrathCount, doEffects);
            }
        }

        public override void SetControls() {
            foreach (Ability ability in abilities) {
                var flourish = ability as Flourish;
                if (flourish != null) {
                    if (flourish.IsActive) {
                        player.controlRight = false;
                        player.controlLeft = false;
                        player.controlJump = false;
                        player.controlUp = false;
                        player.controlDown = false;
                    }
                }
            }
        }

        public override void LevelUp() {
            level += 1;
            TalentSelect();
        }

        public override void TalentSelect() {
            switch (level) {
                case 1:
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