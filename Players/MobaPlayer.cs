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
using TerrariaMoba.Enums;
using TerrariaMoba.Packets;
using TerrariaMoba.Stats;
using TerrariaMoba.UI;
using static TerrariaMobaUtils;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Players {
    public class MobaPlayer : ModPlayer {
        //General
        public CharacterEnum CharacterSelected;
        public Character MyCharacter;
        public bool AbilityOneUsed = false;
        public bool AbilityTwoUsed = false;
        public bool UltimateUsed = false;
        public bool CharacterPicked = false;
        public int PlayerLastHurt = -1;
        public bool Silenced = false;
        public bool Weakened = false;
        public bool InProgress = false;
        public int GameTime = 0;

        //Custom Stats
        public List<Tuple<String, float, int>> BonusDamageList; //I need to rewrite this so it's cleaner
        public List<Tuple<String, float, int>>  ReducedDamageList;
        public CustomStats customStats;

        public override void Initialize() {
            BonusDamageList = new List<Tuple<String, float, int>>();
            ReducedDamageList = new List<Tuple<String, float, int>>();
            CharacterSelected = CharacterEnum.Null;
            customStats = new CustomStats();
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)Message.SyncCustomStats);
            packet.Write((byte)player.whoAmI);
            customStats.Send(packet);
            packet.Send();
        }

        public override void SendClientChanges(ModPlayer clientPlayer) {
            MobaPlayer clone = clientPlayer as MobaPlayer;
            if (!customStats.Equals(clone.customStats)) {
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)Message.SyncCustomStats);
                packet.Write((byte)player.whoAmI);
                customStats.Send(packet);
                packet.Send();
            }
        }

        public override void OnEnterWorld(Player player) {
            TerrariaMoba.Instance.MobaBar = null;
            TerrariaMoba.Instance.MobaBar = new MobaBar();
            TerrariaMoba.Instance.HideBar();
        }

        public override void OnRespawn(Player player) {
            player.statLife = player.statLifeMax;
        }

        public override void ResetEffects() {
            customStats.percentThorns = 0f;
            Silenced = false;
            Weakened = false;
            if (InProgress && CharacterPicked) {
                player.statLifeMax2 = customStats.maxHealth;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                if (MyCharacter.AbilityOneCooldownTimer == 0) {
                    MyCharacter.AbilityOneOnCast(player);
                    SyncAbilitiesPacket.Write(0, player.whoAmI);
                }
                else {
                    Main.PlaySound(25);
                }
            }
            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                if (MyCharacter.AbilityTwoCooldownTimer == 0) {
                    MyCharacter.AbilityTwoOnCast(player);
                    SyncAbilitiesPacket.Write(1, player.whoAmI);
                }
                else {
                    Main.PlaySound(25);
                }
            }
            if (TerrariaMoba.UltimateHotkey.JustPressed) {
                if (MyCharacter.UltimateCooldownTimer == 0) {
                    MyCharacter.UltimateOnCast(player);
                    SyncAbilitiesPacket.Write(2, player.whoAmI);
                }
                else {
                    Main.PlaySound(25);
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
                AssignCharacter(ref MyCharacter, CharacterEnum.Sylvia, player);
            }

            if (TerrariaMoba.OpenCharacterSelect.JustPressed) {
                if (TerrariaMoba.Instance.SelectInterface.CurrentState == null && !CharacterPicked) {
                    TerrariaMoba.Instance.ShowSelect();
                }
                else {
                    TerrariaMoba.Instance.HideSelect();
                }
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
            if (CharacterPicked && InProgress) {
                if (MyCharacter.AbilityOneCooldownTimer > 0) {
                    MyCharacter.AbilityOneCooldownTimer--;
                }
                if (MyCharacter.AbilityTwoCooldownTimer > 0) {
                    MyCharacter.AbilityTwoCooldownTimer--;
                }
                if (MyCharacter.UltimateCooldownTimer > 0) {
                    MyCharacter.UltimateCooldownTimer--;
                }
                GameTime++;
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

        public override void NaturalLifeRegen(ref float regen) {
            regen *= 0;
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
            EditDamage(ref damage);
            target.GetModPlayer<MobaPlayer>().DamageOverride(damage, target, player.whoAmI, true);
            SyncPvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
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
                
                if (customStats.percentThorns > 0f && sendThorns) {
                    target.GetModPlayer<MobaPlayer>().DamageOverride((int)(damage * customStats.percentThorns), Main.player[killer], target.whoAmI, false);
                    SyncPvpHitPacket.Write(killer, (int)(damage * customStats.percentThorns), target.whoAmI, false);
                }

                int armor = target.GetModPlayer<MobaPlayer>().customStats.armor;
                damage *= ((100 - armor) / 100);
                target.statLife -= damage;

                CombatText.NewText(target.Hitbox, Color.OrangeRed, damage);
                
                if (target.statLife <= 0) {
                    target.KillMe(PlayerDeathReason.ByPlayer(PlayerLastHurt), damage, 1, true);
                }
                
                if (sendThorns) {
                    Main.PlaySound(1, target.position);
                    target.immune = true;
                    target.immuneTime = 8;
                }
            }
        }
        
        public void StartGame() {
            MyCharacter.ChooseCharacter();
            InProgress = true;
            TerrariaMoba.Instance.ShowBar();
            TerrariaMoba.Instance.MobaBar.SetIcons();
        }
    }
}