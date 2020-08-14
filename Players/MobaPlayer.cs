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
using TerrariaMoba.Effects;
using TerrariaMoba.Enums;
using TerrariaMoba.Packets;
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

        public SylviaEffects SylviaEffects;
        public MarieEffects MarieEffects;
        public FlibnobEffects FlibnobEffects;

        //Custom Stats
        public float percentThorns = 0f;
        //public int shield = 0;

        public int maxHealth = 0;
        public int bonusHealth = 0;
        public float lifeRegen = 0;
        public int lifeRegenTimer = 0;
        public float lifeDegen = 0;
        public int lifeDegenTimer = 0;

        public int maxResource = 0;
        public int currentResource = 0;
        public float resourceRegen = 0;
        public int resourceRegenTimer = 0;
        public float resourceDegen = 0;
        public int resourceDegenTimer = 0;
        
        public int armor = 0;
        
        public override void Initialize() {
            CharacterSelected = CharacterEnum.Null;
            SylviaEffects = new SylviaEffects();
            MarieEffects = new MarieEffects();
            FlibnobEffects = new FlibnobEffects();
        }

        public override void clientClone(ModPlayer clientClone) {
            MobaPlayer clone = clientClone as MobaPlayer;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {

        }

        public override void SendClientChanges(ModPlayer clientPlayer) {

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
            maxHealth = 0;
            bonusHealth = 0;
            lifeRegen = 0;
            lifeDegen = 0;
            maxResource = 0;
            resourceRegen = 0;
            resourceDegen = 0;

            percentThorns = 0f;
            Silenced = false;
            Weakened = false;
            IsChanneling = false;

            SylviaEffects.ResetEffects();
            MarieEffects.ResetEffects();
            FlibnobEffects.ResetEffects();
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                MyCharacter.HandleAbility(0);
                
            }
            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                MyCharacter.HandleAbility(1);
            }
            if (TerrariaMoba.UltimateHotkey.JustPressed) {
                MyCharacter.HandleAbility(2);
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

        }
        
        public override void PreUpdate() {
            if (CharacterPicked && InProgress) {
                foreach (Ability ability in MyCharacter.abilities.Where(ability => ability.IsActive)) {
                    ability.Using();
                    if (Main.myPlayer == ability.player.whoAmI && ability.NeedsSyncing) {
                        int index = Array.IndexOf(MyCharacter.abilities, ability);
                        int whoAmI = ability.player.whoAmI;
                        byte[] abilitySpecific = ability.WriteAbility();
                        int length = abilitySpecific.Length;
                        
                        Packets.ReadWriteAbilityPacket.Write(index, whoAmI, length, abilitySpecific);
                    }
                }
                
                foreach (Ability ability in MyCharacter.abilities.Where(ability => ability.Cooldown > 0)) {
                    ability.Cooldown--;
                }
                
                MyCharacter.PreUpdate();
            }
        }

        public override void PostUpdateEquips() {
            if (CharacterPicked && InProgress) {
                MyCharacter.PostUpdateEquips();
                MyCharacter.UpdateBaseStats();
                
                player.statLifeMax2 = maxHealth + bonusHealth;

                if (lifeRegenTimer == 30) {
                    player.statLife += (int)(lifeRegen / 2);
                    lifeRegenTimer = 0;
                }

                if (lifeDegenTimer == 30) {
                    player.statLife -= (int)(lifeDegen / 2);
                    lifeDegenTimer = 0;
                                    
                    if (player.statLife <= 0) {
                        if (PlayerLastHurt >= 0 && PlayerLastHurt <= 255) {
                            player.KillMe(PlayerDeathReason.ByPlayer(PlayerLastHurt), lifeDegen, 0, true);
                        }
                    }
                }
                
                if (resourceRegenTimer == 30) {
                    currentResource += (int)(resourceRegen / 2);
                    resourceRegenTimer = 0;
                }

                if (resourceDegenTimer == 30) {
                    currentResource -= (int)(resourceDegen / 2);
                    resourceDegenTimer = 0;
                }

                if (currentResource > maxResource) {
                    currentResource = maxResource;
                }
                else if (currentResource < 0) {
                    currentResource = 0;
                }

                lifeRegenTimer += 1;
                lifeDegenTimer += 1;
            }
        }

        public override void PostUpdate() {
            base.PostUpdate();
        }

        public override void PostUpdateBuffs() {
            if (CharacterPicked && InProgress) {
                MyCharacter.PostUpdateBuffs();
            }

            if (MarieEffects.LacusianBlessing) {
                player.statDefense += 12;
                player.lifeRegen += 8;
                player.allDamageMult += (float)0.16;
            }
        }

        public override void PostUpdateRunSpeeds() {
            if (CharacterPicked && InProgress) {
                MyCharacter.PostUpdateRunSpeeds();
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
            PvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
            MyCharacter.ModifyHitPvpWithProj(proj, target, ref damage, ref crit);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
            EditDamage(ref damage);
            target.GetModPlayer<MobaPlayer>().DamageOverride(damage, target, player.whoAmI, true);
            PvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
        }
        
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
            //JunglesWrath
            if (SylviaEffects.JunglesWrath) {
                r *= (0.7f - (SylviaEffects.JunglesWrathCount * 0.1f));
                g *= (0.7f -(SylviaEffects.JunglesWrathCount * 0.1f));
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

        public override float UseTimeMultiplier(Item item) {
            if (CharacterPicked && InProgress) {
                return MyCharacter.UseTimeMultiplier(item);
            }
            return 1f;
        }
        
        // For use later when I rework Marie's ultimate.
        /*
        public static readonly PlayerLayer MiscEffectsBack = new PlayerLayer("TerrariaMoba", "MiscEffectsBack", PlayerLayer.MiscEffectsBack, delegate(PlayerDrawInfo drawInfo) {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("TerrariaMoba");
            MobaPlayer modPlayer = drawPlayer.GetModPlayer<MobaPlayer>();
            
            if (modPlayer.MarieEffects.LacusianBlessing) {
                Texture2D texture = mod.GetTexture("Textures/Marie/GoddessOfLacusia");

                int num140 = (int)(((float)drawPlayer.miscCounter / 300f * 6.28318548f).ToRotationVector2().Y * 6f);
                
                Vector2 drawXY = new Vector2((int)(drawInfo.position.X - Main.screenPosition.X - drawPlayer.bodyFrame.Width / 2 + drawPlayer.width / 2), (int)(drawInfo.position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2((drawPlayer.bodyFrame.Width / 2), (drawPlayer.bodyFrame.Height / 2));
                drawXY += new Vector2(-(float)drawPlayer.direction * 10f, (float)(-20 + num140));
                
                DrawData data = new DrawData(texture, drawXY, null, Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), drawPlayer.bodyRotation, texture.Size() / 2f, 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }
        });
        */
        
        public static readonly PlayerLayer MiscEffects = new PlayerLayer("TerrariaMoba", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate(PlayerDrawInfo drawInfo) {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("TerrariaMoba");
            MobaPlayer modPlayer = drawPlayer.GetModPlayer<MobaPlayer>();

            if (modPlayer.SylviaEffects.EnsnaringVines) {
                Texture2D texture = mod.GetTexture("Textures/Sylvia/EnsnaringVines");
                
                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.position.Y + (drawPlayer.height - 2f) - Main.screenPosition.Y);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }
            
            if (modPlayer.FlibnobEffects.TitaniumShell) {
                Texture2D texture = mod.GetTexture("Textures/Flibnob/TitaniumShell");

                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }
        });
        
        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            //MiscEffectsBack.visible = true;
            //layers.Insert(0, MiscEffectsBack);
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

                float damage = sourceDamage;
                
                if (percentThorns > 0f && sendThorns) {
                    target.GetModPlayer<MobaPlayer>().DamageOverride((int)(damage * percentThorns), Main.player[killer], target.whoAmI, false);
                    PvpHitPacket.Write(killer, (int)(damage * percentThorns), target.whoAmI, false);
                }

                int otherArmor = target.GetModPlayer<MobaPlayer>().armor;
                damage *= (100 - otherArmor) / 100f;
                target.statLife -= (int) damage;

                CombatText.NewText(target.Hitbox, Color.OrangeRed, (int) damage);
                
                if (target.statLife <= 0) {
                    target.KillMe(PlayerDeathReason.ByPlayer(PlayerLastHurt), (int) damage, 1, true);
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