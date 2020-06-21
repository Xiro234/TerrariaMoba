using System;
using System.Collections.Generic;
using TerrariaMoba;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ID;
using TerrariaMoba.Abilities;
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
        public bool IsChanneling = false;
        public bool InProgress = false;
        public int GameTime = 0;
        
        public bool JunglesWrath = false;
        public int JunglesWrathCount = 1;
        public bool VerdantFury = false;
        public bool EnsnaringVines = false;

        public bool LacusianBlessing = false;

        //Custom Stats
        public CustomStats customStats;

        public override void Initialize() {
            CharacterSelected = CharacterEnum.Null;
            customStats = new CustomStats();
        }

        public override void clientClone(ModPlayer clientClone) {
            MobaPlayer clone = clientClone as MobaPlayer;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
            ModPacket statPacket = mod.GetPacket();
            statPacket.Write((byte)Message.SyncCustomStats);
            statPacket.Write((byte)player.whoAmI);
            customStats.Send(statPacket);
            statPacket.Send();
           
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
            IsChanneling = false;
            if (InProgress && CharacterPicked) {
                player.statLifeMax2 = customStats.maxHealth;
            }
            
            VerdantFury = false;
            JunglesWrath = false;
            EnsnaringVines = false;

            LacusianBlessing = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                MyCharacter.abilities[0].Start(0);
                
            }
            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                MyCharacter.abilities[1].Start(1);
            }
            if (TerrariaMoba.UltimateHotkey.JustPressed) {
                MyCharacter.abilities[2].Start(2);
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
        
        public override void UpdateBadLifeRegen() {
            //JunglesWrath
            if (JunglesWrath) {
                if (player.lifeRegen > 0) {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen -= 4 * JunglesWrathCount;
            }
        }
        
        public override void PreUpdate() {
            if (CharacterPicked && InProgress) {
                foreach (Ability ability in MyCharacter.abilities.Where(ability => ability.Active)) {
                    ability.InUse();
                    if (Main.myPlayer == ability.player.whoAmI && ability.NeedsSyncing) {
                        int index = Array.IndexOf(MyCharacter.abilities, ability);
                        int whoAmI = ability.player.whoAmI;
                        byte[] abilitySpecific = ability.WriteAbility();
                        int length = abilitySpecific.Length;
                        
                        Packets.SyncAbilityValues.Write(index, whoAmI, length, abilitySpecific);
                    }
                }
                
                foreach (Ability ability in MyCharacter.abilities.Where(ability => ability.Cooldown > 0)) {
                    ability.Cooldown--;
                }
                MyCharacter.PreUpdate();
            }
        }

        public override void PostUpdate() {
            base.PostUpdate();
        }

        public override void PostUpdateBuffs() {
            if (!JunglesWrath) {
                JunglesWrathCount = 1;
            }
            if (LacusianBlessing) {
                player.statDefense += 60; //12
                player.lifeRegen += 20; //8
                player.allDamageMult += (float)0.52; //0.16
            }
        }

        public override void PreUpdateBuffs() {
            base.PreUpdateBuffs();
            /*
            if (LacusianBlessing) {
                player.statDefense += 12;
                player.lifeRegen += 8;
                player.allDamageMult += (float)0.16;
            }
            */
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
        
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
            //JunglesWrath
            if (JunglesWrath) {
                r *= (0.7f - (JunglesWrathCount * 0.1f));
                g *= (0.7f -(JunglesWrathCount * 0.1f));
            }
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {
            if (CharacterPicked && InProgress) {
                return MyCharacter.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage,
                    ref knockBack);
            }
            else {
                return true;
            }
        }

        public static readonly PlayerLayer MiscEffects = new PlayerLayer("TerrariaMoba", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate(PlayerDrawInfo drawInfo) {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("TerrariaMoba");
            MobaPlayer modPlayer = drawPlayer.GetModPlayer<MobaPlayer>();

            if (modPlayer.EnsnaringVines) {
                Texture2D texture = mod.GetTexture("Textures/Sylvia/EnsnaringVines");
                
                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.position.Y + (drawPlayer.height - 2f) - Main.screenPosition.Y);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }
        });
        
        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            MiscEffects.visible = true;
            layers.Add(MiscEffects);

            if (CharacterPicked && InProgress) {
                MyCharacter.ModifyDrawLayers(layers);
            }
        }
        
        public override void SetControls() {
            //EnsnaringVines
            if (IsChanneling) {
                player.controlRight = false;
                player.controlLeft = false;
                player.controlJump = false;
                player.controlUp = false;
                player.controlDown = false;
            }

            if (CharacterPicked && InProgress) {
                MyCharacter.SetControls();
            }
        }

        public void EditDamage(ref int damage) {
            float finalDamageChange = 1f;

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