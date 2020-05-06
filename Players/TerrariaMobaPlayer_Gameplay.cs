using TerrariaMoba;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using TerrariaMoba.Characters;
using TerrariaMoba.Stats;
using TerrariaMoba.Utils;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Players {
    partial class TerrariaMobaPlayer_Gameplay : ModPlayer {
        //General
        public TerrariaMobaStats stats;
        public bool AbilityOneUsed = false;
        public bool AbilityTwoUsed = false;
        public bool UltimateUsed = false;
        public bool CharacterPicked = false;
        public int animCounter = -1;

        //Sylvia
        public bool IsSylvia = false;
        public bool VerdantFury = false;
        public bool JunglesWrath = false;
        public int JunglesWrathCount = 1;

        public override void Initialize() {
            stats = new TerrariaMobaStats();
        }

        public override void ResetEffects() {
            VerdantFury = false;
            JunglesWrath = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                if (stats.MyCharacter.AbilityOneCooldownTimer == 0) {
                    stats.MyCharacter.AbilityOne();
                    stats.MyCharacter.AbilityOneCooldownTimer = stats.MyCharacter.AbilityOneCooldown;
                }
                else {
                    Main.NewText(stats.MyCharacter.AbilityOneName + " is on cooldown!");
                }
            }

            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                if (stats.MyCharacter.AbilityTwoCooldownTimer == 0) {
                    stats.MyCharacter.AbilityTwo();
                    stats.MyCharacter.AbilityTwoCooldownTimer = stats.MyCharacter.AbilityTwoCooldown;
                }
                else {
                    Main.NewText(stats.MyCharacter.AbilityTwoName + " is on cooldown!");
                }
            }

            if (TerrariaMoba.LevelTalentOneHotKey.JustPressed) {
                stats.MyCharacter.LevelTalentOne();
            }

            if (TerrariaMoba.LevelTalentTwoHotKey.JustPressed) {
                stats.MyCharacter.LevelTalentTwo();
            }

            if (TerrariaMoba.LevelTalentThreeHotKey.JustPressed) {
                stats.MyCharacter.LevelTalentThree();
            }

            if (TerrariaMoba.BecomeSylvia.JustPressed) {
                stats.MyCharacter = new Sylvia();
                CharacterPicked = true;
            }
        }

        public override void PreUpdate() {
            if (CharacterPicked) {
                if (stats.MyCharacter.AbilityOneCooldownTimer > 0) {
                    if (--stats.MyCharacter.AbilityOneCooldownTimer == 0) {
                        Main.NewText(stats.MyCharacter.AbilityOneName + " is off of cooldown!");
                    }
                }

                if (stats.MyCharacter.AbilityTwoCooldownTimer > 0) {
                    if (--stats.MyCharacter.AbilityTwoCooldownTimer == 0) {
                        Main.NewText(stats.MyCharacter.AbilityTwoName + " is off of cooldown!");
                    }
                }
            }
        }

        public override void PostUpdate() {
            if (AbilityOneUsed == true) {
                if (animCounter != 0) {
                    stats.MyCharacter.AbilityOneAnimation(ref animCounter);
                }
                else {
                    animCounter = -1;
                    AbilityOneUsed = false;
                }
            }

            if (AbilityTwoUsed == true) { }
        }

        public override void UpdateBadLifeRegen() {
            if (JunglesWrath) {
                if (player.lifeRegen > 0) {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen -= 4 * JunglesWrathCount;
            }
        }

        public override void PostUpdateBuffs() {
            if (!JunglesWrath) {
                JunglesWrathCount = 1;
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (this.player.whoAmI != Main.myPlayer) {
                return;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient && pvp) {
                TerrariaMobaUtils.MobaKill(damageSource.SourcePlayerIndex);
            }
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            if (IsSylvia) {
                target.AddBuff(BuffType<Buffs.JunglesWrath>(), 240, false);
            }
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type,
            ref int damage, ref float knockBack) {
            if (CharacterPicked) {
                if (VerdantFury && item.useAmmo == AmmoID.Arrow) {
                    speedX *= SylviaUtils.GetVerdantFuryIncrease();
                    speedY *= SylviaUtils.GetVerdantFuryIncrease();
                }
            }

            return true;
        }

        public override float UseTimeMultiplier(Item item) {
            if (CharacterPicked) {
                if (VerdantFury && item.useAmmo == AmmoID.Arrow) {
                    return SylviaUtils.GetVerdantFuryIncrease();
                }
            }
            return 1f;
        }
    }
}