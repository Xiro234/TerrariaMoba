using System.Collections.Generic;
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
using TerrariaMoba.Packets;
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

        //Sylvia
        public bool IsSylvia = false;
        public bool VerdantFury = false;
        public bool JunglesWrath = false;
        public bool EnrapturingVines = false;
        public bool IsPhasing = false;
        public bool SylviaUlt1 = false;
        public int JunglesWrathCount = 1;
        public int NumberJavelins = 0;

        public override void Initialize() {
            stats = new TerrariaMobaStats();
        }

        public override void ResetEffects() {
            VerdantFury = false;
            JunglesWrath = false;
            EnrapturingVines = false;
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

            if (TerrariaMoba.UltimateHotkey.JustPressed) {
                stats.MyCharacter.Ultimate();
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage,
            ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if (pvp) {
                return false;
            }
            return true;
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
            if (IsSylvia && proj.ranged) {
                target.AddBuff(BuffType<Buffs.JunglesWrath>(), 240, false);
            }

            target.GetModPlayer<TerrariaMobaPlayer_Gameplay>().DamageOverride(damage, target, player.whoAmI);
            SyncPvpHitPacket.Write(target.whoAmI, damage, player.whoAmI);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
            target.GetModPlayer<TerrariaMobaPlayer_Gameplay>().DamageOverride(damage, target, player.whoAmI);
            SyncPvpHitPacket.Write(target.whoAmI, damage, player.whoAmI);
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type,
            ref int damage, ref float knockBack) {
            if (CharacterPicked) {
                if (VerdantFury && item.type == mod.ItemType("SylviaBow")) {
                    speedX *= SylviaUtils.GetVerdantFuryIncrease();
                    speedY *= SylviaUtils.GetVerdantFuryIncrease();
                }

                if (NumberJavelins > 0) {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 5, damage, knockBack, player.whoAmI);
                    NumberJavelins--;
                    if (NumberJavelins == 0) {
                        SylviaUlt1 = false;
                    }

                    return false;
                }
            }

            return true;
        }

        public override float UseTimeMultiplier(Item item) {
            if (CharacterPicked) {
                if (VerdantFury && item.type == mod.ItemType("SylviaBow")) {
                    return SylviaUtils.GetVerdantFuryIncrease();
                }
            }
            return 1f;
        }

        public void DamageOverride(int sourceDamage, Player target, int killer) {
            if (!target.immune) {
                int damage = sourceDamage;

                damage -= (int) (target.statDefense * 0.5);

                if (damage <= 0) {
                    damage = 1;
                }

                target.statLife -= damage;
                Main.PlaySound(1);
                CombatText.NewText(player.Hitbox, Color.OrangeRed, damage);

                if (Main.LocalPlayer.statLife <= 0) {
                    target.KillMe(PlayerDeathReason.ByPlayer(killer), damage, 1, true);
                }

                target.immune = true;
                target.immuneTime = 8;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
            if (JunglesWrath) {
                r *= (0.7f - (JunglesWrathCount * 0.1f));
                g *= (0.7f -(JunglesWrathCount * 0.1f));
            }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            if (IsPhasing) {
                foreach (PlayerLayer layer in layers) {
                    layer.visible = false;
                }
            }
        }

        public override void PreUpdateMovement() {
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

        public override void SetControls() {
            if (EnrapturingVines) {
                player.controlRight = false;
                player.controlLeft = false;
                player.controlJump = false;
                player.controlUp = false;
                player.controlDown = false;
            }
        }

    }
}