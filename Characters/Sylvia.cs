using Terraria.ModLoader;
using TerrariaMoba.Players;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        private bool IsPhasing = false;
        private bool SylviaUlt1 = false;
        private int SylviaUlt1Timer = 0;
        public float VerdantFuryBuff = 1.3f;
        private int VerdantFuryTime = 180;
        public float VerdantFuryIncrease = 0.05f;
        public int JunglesWrathTime = 180;

        public Sylvia(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Sylvia;
        }

        public override void InitializeCharacter() {
            CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaIcon");
            VerdantFuryTime = 20;
        }

        public override void SetPlayer() {
            vanityHead.SetDefaults(208);
            vanityBody.SetDefaults(1853);
            vanityLeg.SetDefaults(1854);
            primary.SetDefaults(TerrariaMoba.Instance.ItemType("SylviaBow"));

            player.Male = false;
            player.hair = 55;
            player.hairColor = new Color(52, 133, 34);
            player.skinColor = new Color(198,134,66);
            player.eyeColor = new Color(84,42,14);
        }

        public override void SetStats() {
            baseMaxLife = 1340;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = 500;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            baseArmor = 0;

            QAbility = new EnsnaringVines(player);
            EAbility = new VerdantFury(player);
            RAbility = new Flourish(player);
            CAbility = new JunglesWrath(player);
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
                speedX *= VerdantFuryBuff;
                speedY *= VerdantFuryBuff;
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
                                TerrariaMoba.Instance.ProjectileType("SylviaUlt1Projectile"), 
                                (int)player.GetModPlayer<MobaPlayer>().SylviaStats.U1JavelinDmg.Value, knockBack, player.whoAmI);
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
                return VerdantFuryBuff;
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
            player.GetModPlayer<MobaPlayer>().SylviaStats.LevelUp();
            baseMaxLife = (int)player.GetModPlayer<MobaPlayer>().SylviaStats.MaxHealth.Value;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = (int)player.GetModPlayer<MobaPlayer>().SylviaStats.MaxResource.Value;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
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