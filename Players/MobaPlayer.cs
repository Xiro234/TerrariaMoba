using System;
using System.Collections.Generic;
using TerrariaMoba;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ID;
using TerrariaMoba.Characters;
using TerrariaMoba.Packets;
using TerrariaMoba.Stats;
using static TerrariaMobaUtils;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Players {
    public class MobaPlayer : ModPlayer {
        //General
        public Character MyCharacter;
        public bool AbilityOneUsed = false;
        public bool AbilityTwoUsed = false;
        public bool UltimateUsed = false;
        public bool CharacterPicked = false;
        public int PlayerLastHurt = -1;
        public bool Silenced = false;
        public bool Weakened = false;

        //Custom Stats
        public float PercentThorns = 0f;
        public List<Tuple<String, float, int>> BonusDamageList;
        public List<Tuple<String, float, int>>  ReducedDamageList;

        //Players
        public int AllySylvia = -1;
        public int EnemySylvia = -1;

        public override void Initialize() {
            BonusDamageList = new List<Tuple<String, float, int>>();
            ReducedDamageList = new List<Tuple<String, float, int>>();
        }

        public override void ResetEffects() {
            PercentThorns = 0f;
            Silenced = false;
            Weakened = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                if (MyCharacter.AbilityOneCooldownTimer == 0) {
                    MyCharacter.AbilityOne();
                    MyCharacter.AbilityOneCooldownTimer = MyCharacter.AbilityOneCooldown;
                }
                else {
                    Main.NewText(MyCharacter.AbilityOneName + " is on cooldown!");
                }
            }
            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                if (MyCharacter.AbilityTwoCooldownTimer == 0) {
                    MyCharacter.AbilityTwo();
                    MyCharacter.AbilityTwoCooldownTimer = MyCharacter.AbilityTwoCooldown;
                }
                else {
                    Main.NewText(MyCharacter.AbilityTwoName + " is on cooldown!");
                }
            }
            if (TerrariaMoba.LevelTalentOneHotKey.JustPressed) {
                MyCharacter.LevelTalentOne();
            }
            if (TerrariaMoba.LevelTalentTwoHotKey.JustPressed) {
                MyCharacter.LevelTalentTwo();
            }
            if (TerrariaMoba.LevelTalentThreeHotKey.JustPressed) {
                MyCharacter.LevelTalentThree();
            }
            if (TerrariaMoba.BecomeSylvia.JustPressed) {
                AssignCharacter(ref MyCharacter, "sylvia", player);
            }
            if (TerrariaMoba.UltimateHotkey.JustPressed) {
                MyCharacter.Ultimate();
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
                if (MyCharacter.AbilityOneCooldownTimer > 0) {
                    if (--MyCharacter.AbilityOneCooldownTimer == 0) {
                        Main.NewText(MyCharacter.AbilityOneName + " is off of cooldown!");
                    }
                }
                if (MyCharacter.AbilityTwoCooldownTimer > 0) {
                    if (--MyCharacter.AbilityTwoCooldownTimer == 0) {
                        Main.NewText(MyCharacter.AbilityTwoName + " is off of cooldown!");
                    }
                }
            }
        }

        public override void PostUpdateEquips() {
            if (CharacterPicked) {
                if (AllySylvia == player.whoAmI) {
                    //Sylvia Talent [0,1]: Graceful Leap
                    if (MyCharacter.talentArray[0, 1]) {
                        player.doubleJumpCloud = true; //doesn't work, needs to sync somewhere idk
                    }
                    //Sylvia Talent [0,2]: Thorns Embrace
                    if (MyCharacter.talentArray[0, 2]) {
                        player.statDefense += 6;
                        PercentThorns += 0.15f;
                    }
                }
            }
        }

        public override void PostUpdateBuffs() {
            //Weakened
            if (ReducedDamageList.Count > 0) {
                for (int i = 0; i < ReducedDamageList.Count; i++) {
                    ReducedDamageList[i] = new Tuple<string, float, int>(ReducedDamageList[i].Item1, ReducedDamageList[i].Item2, ReducedDamageList[i].Item3 - 1);
                    
                    if (ReducedDamageList[i].Item3 == 0) {
                        ReducedDamageList.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            if (BonusDamageList.Count > 0) {
                for (int i = 0; i < BonusDamageList.Count; i++) {
                    ReducedDamageList[i] = new Tuple<string, float, int>(ReducedDamageList[i].Item1, ReducedDamageList[i].Item2, ReducedDamageList[i].Item3 - 1);

                    if (BonusDamageList[i].Item3 == 0) {
                        BonusDamageList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (this.player.whoAmI != Main.myPlayer) {
                return;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient && pvp) {
                TerrariaMobaUtils.MobaKill(damageSource.SourcePlayerIndex);
            }
            else {
                TerrariaMobaUtils.MobaKill(PlayerLastHurt);
            }
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            if (CharacterPicked) {
                if (AllySylvia == player.whoAmI) {
                    //Jungles Wrath
                    if (proj.ranged) {
                        target.AddBuff(BuffType<Buffs.JunglesWrath>(), 240, false);
                    }
                }
            }
            
            EditDamage(ref damage);
            target.GetModPlayer<MobaPlayer>().DamageOverride(damage, target, player.whoAmI, true);
            SyncPvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
            if (CharacterPicked) {
            }

            EditDamage(ref damage);
            target.GetModPlayer<MobaPlayer>().DamageOverride(damage, target, player.whoAmI, true);
            SyncPvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
        }

        public void EditDamage(ref int damage) {
            float finalDamageChange = 1f;

            foreach (var item in ReducedDamageList) {
                finalDamageChange *= (1f - item.Item2);
            }
            
            foreach (var item in BonusDamageList) {
                finalDamageChange *= item.Item2;
            }
            
            damage = (int)(finalDamageChange * damage);
        }

        public void DamageOverride(int sourceDamage, Player target, int killer, bool sendThorns) {
            if (!target.immune) {
                PlayerLastHurt = killer;

                int damage = sourceDamage;
                
                if (PercentThorns > 0f && sendThorns) {
                    target.GetModPlayer<MobaPlayer>().DamageOverride((int)(damage * PercentThorns), Main.player[killer], target.whoAmI, false);
                    SyncPvpHitPacket.Write(killer, (int)(damage * PercentThorns), target.whoAmI, false);
                }
                
                damage -= (int) (target.statDefense * 0.5);
                if (damage <= 0) {
                    damage = 1;
                }

                target.statLife -= damage;

                if (target.statLife <= 0) {
                    target.KillMe(PlayerDeathReason.ByPlayer(PlayerLastHurt), damage, 1, true);
                }
                
                CombatText.NewText(target.Hitbox, Color.OrangeRed, damage);
                
                if (sendThorns) {
                    Main.PlaySound(1, target.position);
                    target.immune = true;
                    target.immuneTime = 8;
                }
            }
        }
    }
}