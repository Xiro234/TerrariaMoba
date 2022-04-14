using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using TerrariaMoba.Abilities;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Network;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Statistic;

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
        }

        public override void OnRespawn(Player Player) {
            Player.statLife = Player.statLifeMax;
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback) {
            damage = (int)Player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_DAMAGE);
            velocity.Normalize();
            velocity *= Player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_VELOCITY);

            AbilityEffectManager.ModifyShootStats(Player, ref item, ref position, ref velocity, ref type, ref damage,
                ref knockback);
        }

        public override void PreUpdateMovement() {
            if (MobaSystem.MatchInProgress) {
                Player.jumpSpeed *= Player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.JUMP_SPEED);
            }
        }
        
        public override void PostUpdateRunSpeeds() {
            if (MobaSystem.MatchInProgress) {
                Player.moveSpeed *= Player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.MOVEMENT_SPEED);
                Player.maxRunSpeed *= Player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.MOVEMENT_SPEED);
            }
        }

        public override void ResetEffects() {
            if (MobaSystem.MatchInProgress) {
                TickStatusEffects();
                TickAbilities();

                AbilityEffectManager.ResetEffects(Player);
                SetPlayerHealth();
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotkey.JustPressed) {
                Hero?.BasicAbilityOne.OnCast();

                if (Main.netMode != NetmodeID.SinglePlayer) {
                    NetworkHandler.SendAbilityCast(0, Player.whoAmI);
                }
            }
            
            if (TerrariaMoba.AbilityTwoHotkey.JustPressed) {
                Hero?.BasicAbilityTwo.OnCast();

                if (Main.netMode != NetmodeID.SinglePlayer) {
                    NetworkHandler.SendAbilityCast(1, Player.whoAmI);
                }
            }

            if (TerrariaMoba.AbilityThreeHotkey.JustPressed) {
                Hero?.BasicAbilityThree.OnCast();

                if (Main.netMode != NetmodeID.SinglePlayer) {
                    NetworkHandler.SendAbilityCast(2, Player.whoAmI);
                }
            }

            if (TerrariaMoba.UltimateHotkey.JustPressed) {
                Hero?.Ultimate.OnCast();
                
                if (Main.netMode != NetmodeID.SinglePlayer) {
                    NetworkHandler.SendAbilityCast(3, Player.whoAmI);
                }
            }

            if (TerrariaMoba.TraitHotkey.JustPressed) {
                Hero?.Trait.OnCast();

                if (Main.netMode != NetmodeID.SinglePlayer) {
                    NetworkHandler.SendAbilityCast(4, Player.whoAmI);
                }
            }
            
            if (TerrariaMoba.OpenCharacterSelect.JustPressed) {
                if (MobaSystem.SelectInterface.CurrentState == null && Hero == null) {
                    MobaSystem.ShowSelect();
                }
                else {
                    MobaSystem.HideSelect();
                }
            }
            
            /*
            //talent selection
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
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
            ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if (pvp) {
                return false;
            }
            return true;
        }

        public override void PostUpdateBuffs() {
            if (MobaSystem.MatchInProgress) {
                RegenLife();
                RegenResource();
            }
        }

        public override void NaturalLifeRegen(ref float regen) {
            regen *= 0;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (Main.netMode == NetmodeID.MultiplayerClient && pvp) {
            }
            else {
                AbilityEffectManager.Kill(Player, damage, hitDirection, pvp, damageSource);
            }
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var damageTypeProj = proj.GetGlobalProjectile<DamageTypeGlobalProj>();
            int physicalDamage = damageTypeProj.PhysicalDamage;
            int magicalDamage = damageTypeProj.MagicalDamage;
            int trueDamage = damageTypeProj.TrueDamage;
            
            if (physicalDamage == 0 && magicalDamage == 0 && trueDamage == 0) {
                Main.NewText(proj.Name + " has not set any damage types! Please contact developers immediately.", Color.Red);
            }
            
            AbilityEffectManager.ModifyHitPvpWithProj(Player, proj, target, ref damage, ref crit);

            target.GetModPlayer<MobaPlayer>().TakePvpDamage(physicalDamage, magicalDamage, trueDamage, Player.whoAmI, false);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
            target.GetModPlayer<MobaPlayer>().TakePvpDamage(damage, 0, 0, Player.whoAmI, false);
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
            Texture2D healthBar = ModContent.Request<Texture2D>("TerrariaMoba/Textures/PlayerHealthBar").Value;
            Vector2 barPos = new Vector2(Player.Top.X - Main.screenPosition.X - (healthBar.Width/2),
                Player.Top.Y - Main.screenPosition.Y - 20);
            Main.spriteBatch.Draw(healthBar, barPos, Color.White);

            float quotient = Utils.Clamp((float) Player.statLife / Player.statLifeMax2, 0f, 1f);

            int left = 0;
            int right = healthBar.Width;
            int top = 0;
            int bottom = healthBar.Height;
            int steps = (int) ((right - 12) * quotient);
            Color gradA;
            Color gradB;
            gradA = Color.DarkGreen;
            gradB = Color.Lime;
            
            int stepsPerHundred = (int)(steps / (Player.statLifeMax2 / 100f));
            int countPerBar = 0;
            
            for (int i = 0; i < steps; i++) {
                float percent = (float) i / (right - left);
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Vector2(barPos.X + i + 6, barPos.Y + 2),
                    new Rectangle(0, 0, 1, 6),
                    Color.Lerp(gradA, gradB, percent));

                /*if (i % stepsPerHundred == 0 && i != 0) {
                    countPerBar++;
                    if (countPerBar % 10 == 0) {
                        Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Vector2(barPos.X + i + 6, barPos.Y + 2),
                            new Rectangle(0, 0, 1, 6),
                            Color.Black);
                    }
                    else {
                        Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Vector2(barPos.X + i + 6, barPos.Y + 2),
                            new Rectangle(0, 0, 1, 4),
                            Color.Black);
                    }
                }*/
            }
            AbilityEffectManager.DrawEffects(Player, drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage,
            float knockback) {
            return AbilityEffectManager.Shoot(Player, ref item, ref source, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override float UseSpeedMultiplier(Item item) {
            return AbilityEffectManager.UseSpeedMultiplier(Player, ref item);
        }

        // For use later when I rework Marie's ultimate.
        /*
        public static readonly PlayerDrawLayer MiscEffectsBack = new PlayerDrawLayer("TerrariaMoba", "MiscEffectsBack", PlayerDrawLayer.MiscEffectsBack, delegate(PlayerDrawSet drawInfo) {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("TerrariaMoba");
            MobaPlayer modPlayer = drawPlayer.GetModPlayer<MobaPlayer>();
            
            if (modPlayer.MarieEffects.LacusianBlessing) {
                Texture2D texture = Mod.Assets.Request<Texture2D>("Textures/Marie/GoddessOfLacusia").Value;
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

        /*public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo) {

            foreach (var effect in EffectList) {
                List<PlayerDrawLayer> playerLayers = new List<PlayerDrawLayer>();
                effect.GetListOfPlayerDrawLayers(playerLayers);
                foreach (var effectLayer in playerLayers) {
                    if (effectLayer != null) {
                        effectLayer.visible = true;
                        layers.Add(effectLayer);
                        layers.Add(effect.GetEffectBar());
                    }
                }
                //TODO - Add checking for invisiblity and add constants somewhere for where elements should be
                //TODO - Work in new player-layer system
            }
        }*/
        
        
        public override void SetControls() {
           AbilityEffectManager.SetControls(Player);
        }
        

        public void TakePvpDamage(int physicalDamage, int magicalDamage, int trueDamage, int killer, bool noBroadcast) {
            if (!Player.immune) {
                AbilityEffectManager.TakePvpDamage(Player, ref physicalDamage, ref magicalDamage, ref trueDamage, ref killer);
                int mitigatedPhysical = (int)Math.Ceiling(physicalDamage - physicalDamage * GetCurrentAttributeValue(AttributeType.PHYSICAL_ARMOR) * 0.01f);
                int mitigatedMagical = (int)Math.Ceiling(magicalDamage - magicalDamage * GetCurrentAttributeValue(AttributeType.MAGICAL_ARMOR) * 0.01f);
                
                if (mitigatedPhysical > 0) {
                    CombatText.NewText(Player.Hitbox, Color.Maroon, mitigatedPhysical);
                }

                if (mitigatedMagical > 0) {
                    CombatText.NewText(Player.Hitbox, Color.DodgerBlue, mitigatedMagical);
                }

                if (trueDamage > 0) {
                    CombatText.NewText(Player.Hitbox, Color.Goldenrod, trueDamage);
                }

                int dealtDamage = mitigatedPhysical + mitigatedMagical + trueDamage;
                Player.statLife -= dealtDamage;
                
                if (Player.statLife <= 0) {
                    Player.KillMe(PlayerDeathReason.ByPlayer(killer), dealtDamage, 1, true);
                }
                
                SoundEngine.PlaySound(1, Player.position);

                if (!noBroadcast) {
                    NetworkHandler.SendPvpHit(physicalDamage, magicalDamage, trueDamage, Player.whoAmI, killer);
                }
            }
        }

        public void HealMe(int amount, bool doText) {
            if(doText) {
                CombatText.NewText(Player.Hitbox, CombatText.HealLife, amount, false);
            }
            Player.statLife += amount;
            if (Player.statLife > Player.statLifeMax2) {
                Player.statLife = Player.statLifeMax2;
            }
        }
    }
}