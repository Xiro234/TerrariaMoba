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
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Players {
    partial class TerrariaMobaPlayer_Gameplay : ModPlayer {
        //General
        public Character MyCharacter;
        public bool AbilityOneUsed = false;
        public bool AbilityTwoUsed = false;
        public bool UltimateUsed = false;
        public bool CharacterPicked = false;
        public int PlayerLastHurt = -1;
        
        //Custom Stats
        public float PercentThorns = 0f;

        //Sylvia
        public SylviaStats MySylviaStats = new SylviaStats();
        public bool VerdantFury = false;
        public bool JunglesWrath = false;
        public bool EnrapturingVines = false;
        public bool IsPhasing = false;
        public bool SylviaUlt1 = false;
        public int SylviaUlt1Timer = 0;
        public int JunglesWrathCount = 1;
        public int NumberJavelins = 0;

        public override void Initialize() {
            
        }

        public override void ResetEffects() {
            VerdantFury = false;
            JunglesWrath = false;
            EnrapturingVines = false;
            PercentThorns = 0f;
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
                MyCharacter = new Sylvia();
                CharacterPicked = true;
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
                if (MyCharacter.CharacterName == "sylvia") {
                    //Flourish
                    if (SylviaUlt1Timer > 0) {
                        SylviaUlt1Timer--;
                        if (SylviaUlt1Timer == 0) {
                            SylviaUlt1 = false;
                            NumberJavelins = 0;
                            SyncSylviaUlt1Packet.Write(player.whoAmI, SylviaUlt1);
                        }
                    }
                }
                else {
                    Main.NewText("Invalid Character Name: PreUpdate");
                }
            }
        }

        public override void PostUpdateEquips() {
            if (CharacterPicked) {
                if (MyCharacter.CharacterName == "sylvia") {
                    //Graceful Leap
                    if (MyCharacter.talentArray[0, 1]) {
                        player.doubleJumpCloud = true;
                    }
                    //Thorns Embrace
                    if (MyCharacter.talentArray[0, 2]) {
                        player.statDefense += 6;
                        PercentThorns += 0.15f;
                    }
                }
            }
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
        

        public override void PostUpdateBuffs() {
            //JunglesWrath
            if (!JunglesWrath) {
                JunglesWrathCount = 0;
            }
            //IsPhasing
            if (IsPhasing) {
                player.immune = true;
                player.immuneTime = 1;
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
                if (MyCharacter.CharacterName == "sylvia") {
                    //JunglesWrath
                    if (proj.ranged) {
                        target.AddBuff(BuffType<Buffs.JunglesWrath>(), 240, false);
                    }
                }
                else {
                    Main.NewText("Invalid Character Name: ModifyHitPvpWithProj");
                }
            }

            target.GetModPlayer<TerrariaMobaPlayer_Gameplay>().DamageOverride(damage, target, player.whoAmI, true);
            SyncPvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
            target.GetModPlayer<TerrariaMobaPlayer_Gameplay>().DamageOverride(damage, target, player.whoAmI, true);
            SyncPvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type,
            ref int damage, ref float knockBack) {
            if (CharacterPicked) {
                if (MyCharacter.CharacterName == "sylvia") {
                    //VerdantFury
                    if (VerdantFury && item.type == mod.ItemType("SylviaBow")) {
                        speedX *= MySylviaStats.GetVerdantFuryIncrease();
                        speedY *= MySylviaStats.GetVerdantFuryIncrease();
                    }
                    //Flourish
                    if (NumberJavelins > 0) {
                        Vector2 velocity = new Vector2();
                        velocity.X = speedX;
                        velocity.Y = speedY;
                        velocity.Normalize();
                        velocity *= 15;

                        Projectile.NewProjectile(position.X, position.Y, velocity.X, velocity.Y,
                            mod.ProjectileType("SylviaUlt1Projectile"), 40, knockBack, player.whoAmI);
                        NumberJavelins--;
                        if (NumberJavelins == 0) {
                            SylviaUlt1 = false;
                            SyncSylviaUlt1Packet.Write(player.whoAmI, SylviaUlt1);
                        }
                        return false;
                    }
                }
                else {
                    Main.NewText("Invalid Character Name: Shoot");
                }
            }
            return true;
        }

        public override float UseTimeMultiplier(Item item) {
            if (CharacterPicked) {
                if (MyCharacter.CharacterName == "sylvia") {
                    //VerdantFury
                    if (VerdantFury && item.type == mod.ItemType("SylviaBow")) {
                        return MySylviaStats.GetVerdantFuryIncrease();
                    }
                }
                else {
                    Main.NewText("Invalid Character Name: UseTimeMultiplier");
                }
            }
            return 1f;
        }

        public void DamageOverride(int sourceDamage, Player target, int killer, bool sendThorns) {
            if (!target.immune) {
                PlayerLastHurt = killer;

                int damage = sourceDamage;
                
                if (PercentThorns > 0f && sendThorns) {
                    target.GetModPlayer<TerrariaMobaPlayer_Gameplay>().DamageOverride((int)(damage * PercentThorns), Main.player[killer], target.whoAmI, false);
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
                if (!sendThorns) {
                    Main.PlaySound(1, target.position);
                    target.immune = true;
                    target.immuneTime = 8;
                }
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
            //JunglesWrath
            if (JunglesWrath) {
                r *= (0.7f - (JunglesWrathCount * 0.1f));
                g *= (0.7f -(JunglesWrathCount * 0.1f));
            }
        }

        public static readonly PlayerLayer MiscEffects = new PlayerLayer("TerrariaMoba", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate(PlayerDrawInfo drawInfo) {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("TerrariaMoba");
            TerrariaMobaPlayer_Gameplay modPlayer = drawPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>();

            if (modPlayer.EnrapturingVines) {
                Texture2D texture = mod.GetTexture("Textures/EnsnaringVines");
                
                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.position.Y + (drawPlayer.height - 2f) - Main.screenPosition.Y);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }
        });
        
        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            //IsPhasing
            MiscEffects.visible = true;
            layers.Add(MiscEffects);
            
            if (IsPhasing) {
                foreach (PlayerLayer layer in layers) {
                    layer.visible = false;
                }
            }
        }

        public override void PreUpdateMovement() {
            if (CharacterPicked) {
                if (MyCharacter.CharacterName == "sylvia") {
                    //Flourish
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
                else {
                    Main.NewText("Invalid Character Name: PreUpdateMovement");
                }
            }
        }

        public override void PostUpdateRunSpeeds() {
            if (CharacterPicked) {
                if (MyCharacter.CharacterName == "sylvia") {
                    if (MyCharacter.talentArray[0, 0]) {
                        player.moveSpeed *= 1.36f;
                        player.accRunSpeed *= 1.36f;
                        player.maxRunSpeed *= 1.36f;
                    }
                }
            }
        }

        public override void SetControls() {
            //EnrapturingVines
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