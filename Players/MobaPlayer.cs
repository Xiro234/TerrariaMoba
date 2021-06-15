using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ID;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Network;
using TerrariaMoba.Statistic;
using TerrariaMoba.UI;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        //General
        public int PlayerLastHurt = -1;
        public int GameTime = 0;

        public int ultTimer = -1;
        public List<Ability> TestAbilities;

        public override void Initialize() {
            EffectList = new List<StatusEffect>();
            TestAbilities = new List<Ability>();
            Stats = new Statistics();
        }

        public override void OnEnterWorld(Player player) {
            TerrariaMoba.Instance.MobaBar = null;
            TerrariaMoba.Instance.MobaBar = new MobaBar();
            //TerrariaMoba.Instance.HideBar();

            TestAbilities.Add(new EnsnaringVinesAbility());
            TestAbilities.Add(new JunglesWrathAbility());
        }

        public override void OnRespawn(Player player) {
            player.statLife = player.statLifeMax;
        }

        public override void ResetEffects() {
            Stats.ResetStats();
            TickStatusEffects();
            TickAbilities();

            AbilityEffectManager.ResetEffects(player);
        }
        
        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                TestAbilities[0].OnCast();
                
                /*
                if (Main.netMode != NetmodeID.MultiplayerClient) {
                    MyCharacter.HandleAbility(MyCharacter.QAbility);
                }
                else {
                    new AbilityCastPacket() {
                        index = MyCharacter.abilities.IndexOf(MyCharacter.QAbility)
                    }.Send();
                }
                */
            }
            /*
            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                if (Main.netMode != NetmodeID.MultiplayerClient) {
                    MyCharacter.HandleAbility(MyCharacter.EAbility);
                }
                else {
                    new AbilityCastPacket() {
                        ability = MyCharacter.EAbility,
                        index = MyCharacter.abilities.IndexOf(MyCharacter.EAbility)
                    }.Send();
                }
            }
            
            if (TerrariaMoba.UltimateHotkey.JustPressed) {
                if (Main.netMode != NetmodeID.MultiplayerClient) {
                    MyCharacter.HandleAbility(MyCharacter.RAbility);
                }
                else {
                    new AbilityCastPacket() {
                        ability = MyCharacter.RAbility,
                        index = MyCharacter.abilities.IndexOf(MyCharacter.RAbility)
                    }.Send();
                }
            }
            
            if (TerrariaMoba.TraitHotkey.JustPressed) {
                if (Main.netMode != NetmodeID.MultiplayerClient) {
                    MyCharacter.HandleAbility(MyCharacter.CAbility);
                }
                else {
                    new AbilityCastPacket() {
                        ability = MyCharacter.CAbility,
                        index = MyCharacter.abilities.IndexOf(MyCharacter.CAbility)
                    }.Send();
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
            */
            if (TerrariaMoba.OpenCharacterSelect.JustPressed) {
                if (TerrariaMoba.Instance.SelectInterface.CurrentState == null && Hero == null) {
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
        
        public override void PostUpdateRunSpeeds() {
            
        }

        public override void PostUpdateBuffs() {
            RegenLife();
            RegenResource();
        }

        public override void NaturalLifeRegen(ref float regen) {
            regen *= 0;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (Main.netMode == NetmodeID.MultiplayerClient && pvp) {
            }
            else {
                //TerrariaMobaUtils.MobaKill(PlayerLastHurt, player.whoAmI);
                AbilityEffectManager.Kill(player, damage, hitDirection, pvp, damageSource);
            }
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            AbilityEffectManager.ModifyHitPvpWithProj(player, proj, target, ref damage, ref crit);
            target.GetModPlayer<MobaPlayer>().TakePvpDamage(damage, player.whoAmI, false);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
            target.GetModPlayer<MobaPlayer>().TakePvpDamage(damage, player.whoAmI, false);
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
            Texture2D healthBar = TerrariaMoba.Instance.GetTexture("Textures/PlayerHealthBar");
            Vector2 barPos = new Vector2(player.Top.X - Main.screenPosition.X - (healthBar.Width/2),
                player.Top.Y - Main.screenPosition.Y - 20);
            Main.spriteBatch.Draw(healthBar, barPos, Color.White);

            float quotient = Utils.Clamp((float) player.statLife / player.statLifeMax2, 0f, 1f);

            int left = 0;
            int right = healthBar.Width;
            int top = 0;
            int bottom = healthBar.Height;
            int steps = (int) ((right - 12) * quotient);
            Color gradA;
            Color gradB;
            gradA = Color.DarkGreen;
            gradB = Color.Lime;
            
            int stepsPerHundred = (int)(steps / (player.statLifeMax2 / 100f));
            int countPerBar = 0;
            
            for (int i = 0; i < steps; i++) {
                float percent = (float) i / (right - left);
                Main.spriteBatch.Draw(Main.magicPixel, new Vector2(barPos.X + i + 6, barPos.Y + 2),
                    new Rectangle(0, 0, 1, 6),
                    Color.Lerp(gradA, gradB, percent));

                /*if (i % stepsPerHundred == 0 && i != 0) {
                    countPerBar++;
                    if (countPerBar % 10 == 0) {
                        Main.spriteBatch.Draw(Main.magicPixel, new Vector2(barPos.X + i + 6, barPos.Y + 2),
                            new Rectangle(0, 0, 1, 6),
                            Color.Black);
                    }
                    else {
                        Main.spriteBatch.Draw(Main.magicPixel, new Vector2(barPos.X + i + 6, barPos.Y + 2),
                            new Rectangle(0, 0, 1, 4),
                            Color.Black);
                    }
                }*/
            }
            AbilityEffectManager.DrawEffects(player, drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {
            return AbilityEffectManager.Shoot(player, item, ref position, ref speedX, ref speedY, ref type, ref damage,
                ref knockBack);
        }

        // For use later when I rework Marie's ultimate.
        /*
        public static readonly PlayerLayer MiscEffectsBack = new PlayerLayer("TerrariaMoba", "MiscEffectsBack", PlayerLayer.MiscEffectsBack, delegate(PlayerDrawInfo drawInfo) {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("TerrariaMoba");
            MobaPlayer modPlayer = drawPlayer.GetModPlayer<MobaPlayer>();
            
            if (modPlayer.MarieEffects.LacusianBlessing) {
                Texture2D texture = mod.GetTexture("Textures/Marie/GoddessOfLacusia");
                SpriteEffects effects;

                if (drawPlayer.direction == 1) {
                    effects = SpriteEffects.None;
                } else {
                    effects = SpriteEffects.FlipHorizontally;
                }
                
                int num140 = (int)(((float)drawPlayer.miscCounter / 300f * 6.28318548f).ToRotationVector2().Y * 6f);
                Vector2 drawXY = new Vector2((int)(drawInfo.position.X - Main.screenPosition.X - drawPlayer.bodyFrame.Width / 2 + drawPlayer.width / 2), (int)(drawInfo.position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2((drawPlayer.bodyFrame.Width / 2), (drawPlayer.bodyFrame.Height / 2));
                drawXY += new Vector2(-(float)drawPlayer.direction * 10f, (float)(-20 + num140));
                
                DrawData data = new DrawData(texture, drawXY, null, Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), drawPlayer.bodyRotation, texture.Size() / 2f, 1f, effects, 0);
                Main.playerDrawData.Add(data);
            }
        });
        */
        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            foreach (var effect in EffectList) {
                List<PlayerLayer> playerLayers = new List<PlayerLayer>();
                effect.GetListOfPlayerLayers(playerLayers);
                foreach (var effectLayer in playerLayers) {
                    if (effectLayer != null) {
                        effectLayer.visible = true;
                        layers.Add(effectLayer);
                        layers.Add(effect.GetEffectBar());
                    }
                }
                //TODO - Add checking for invisiblity and add constants somewhere for where elements should be
            }
        }
        
        public override void SetControls() {
           AbilityEffectManager.SetControls(player);
        }
        

        public void TakePvpDamage(int damage, int killer, bool noBroadcast) {
            if (!player.immune) {
                
                AbilityEffectManager.TakePvpDamage(player, ref damage, ref killer);
                player.statLife -= damage;

                CombatText.NewText(player.Hitbox, Color.OrangeRed, damage);
                
                Main.NewText(player.whoAmI + " " + killer);
                
                if (player.statLife <= 0) {
                    player.KillMe(PlayerDeathReason.ByPlayer(killer), damage, 1, true);
                }
                
                Main.PlaySound(1, player.position);

                if (!noBroadcast) {
                    NetworkHandler.SendPvpHit(damage, player.whoAmI, killer);
                }
            }
        }

        public void StartGame() {
            TerrariaMoba.Instance.ShowBar();
            TerrariaMoba.Instance.MobaBar.SetIcons();
        }

        public void HealMe(int amount, bool doText) {
            //MyCharacter.HealMe(ref amount);
            if(doText) {
                CombatText.NewText(player.Hitbox, CombatText.HealLife, amount, false);
            }
            player.statLife += amount;
            if (player.statLife > player.statLifeMax2) {
                player.statLife = player.statLifeMax2;
            }
        }
    }
}