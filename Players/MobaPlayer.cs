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
            if (MobaSystem.MatchInProgress) {
                if (AbilityEffectManager.OnRespawn(Player)) {
                    SetPlayerHealth();
                    Player.statLife = Player.statLifeMax2;
                    SetPlayerResource();
                }
            }
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback) {
            if (MobaSystem.MatchInProgress) {
                damage = (int)Player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_DAMAGE);
                velocity.Normalize();
                velocity *= Player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_VELOCITY);
                
                /* AbilityEffectManager.ModifyShootStats(Player, ref item, ref position, ref velocity, ref type,
                    ref damage, ref knockback); */
            }
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
                Player.accRunSpeed = Player.maxRunSpeed;
            }
        }

        public override void ResetEffects() {
            if (MobaSystem.MatchInProgress) {
                DisableAbilities = false;
                
                SetPlayerHealth();
                AbilityEffectManager.ResetEffects(Player);
                TickStatusEffects();
                TickAbilities();
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotkey.JustPressed) {
                if (Hero?.BasicAbilityOne.CastIfAble() == true) {
                    if (Main.netMode != NetmodeID.SinglePlayer) {
                        NetworkHandler.SendAbilityCast(0, Player.whoAmI);
                    }
                }
            }
            
            if (TerrariaMoba.AbilityTwoHotkey.JustPressed) {
                if (Hero?.BasicAbilityTwo.CastIfAble() == true) {
                    if (Main.netMode != NetmodeID.SinglePlayer) {
                        NetworkHandler.SendAbilityCast(1, Player.whoAmI);
                    }
                }
            }

            if (TerrariaMoba.AbilityThreeHotkey.JustPressed) {
                if (Hero?.BasicAbilityThree.CastIfAble() == true) {
                    if (Main.netMode != NetmodeID.SinglePlayer) {
                        NetworkHandler.SendAbilityCast(2, Player.whoAmI);
                    }
                }
            }

            if (TerrariaMoba.UltimateHotkey.JustPressed) {
                if (Hero?.Ultimate.CastIfAble() == true) {

                    if (Main.netMode != NetmodeID.SinglePlayer) {
                        NetworkHandler.SendAbilityCast(3, Player.whoAmI);
                    }
                }
            }

            if (TerrariaMoba.TraitHotkey.JustPressed) {
                if (Hero?.Trait.CastIfAble() == true) {
                    if (Main.netMode != NetmodeID.SinglePlayer) {
                        NetworkHandler.SendAbilityCast(4, Player.whoAmI);
                    }
                }
            }

            if(TerrariaMoba.SecondUltHotkey.JustPressed) {
                if (Hero?.SecondUlt.CastIfAble() == true) {
                    if (Main.netMode != NetmodeID.SinglePlayer) {
                        NetworkHandler.SendAbilityCast(5, Player.whoAmI);
                    }
                }
            }

            if (TerrariaMoba.OpenCharacterSelect.JustPressed) {
                if (MobaSystem.SelectInterface.CurrentState == null && Hero == null) {
                    MobaSystem.ShowSelect();
                } else {
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

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter) {
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

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            bool kill = base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);  
            return kill &= AbilityEffectManager.PreKill(Player, damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (Main.netMode == NetmodeID.MultiplayerClient && MobaSystem.MatchInProgress) {
                AbilityEffectManager.Kill(Player, damage, hitDirection, pvp, damageSource);
                EffectList.Clear();
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
            
            AbilityEffectManager.DealPvpDamage(Player, ref physicalDamage, ref magicalDamage, ref trueDamage, target, new DamageSource(proj));

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

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, 
            int type, int damage, float knockback) {
            return AbilityEffectManager.Shoot(Player, item, source, position, velocity, type, damage, knockback);
        }

        public override float UseSpeedMultiplier(Item item) {
            if (MobaSystem.MatchInProgress) {
                return 1 + Player.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_SPEED);
            } else {
                return 1f;
            }
        }

        public override void SetControls() {
           AbilityEffectManager.SetControls(Player);
        }

        public void TakePvpDamage(int physicalDamage, int magicalDamage, int trueDamage, int killer, bool noBroadcast) {
            Logging.PublicLogger.Debug(Main.player[killer].GetModPlayer<MobaPlayer>().Hero.Name +
                " did P: " + physicalDamage + " + M: " + magicalDamage + " + T: " + trueDamage + " damage to " + Hero.Name + "!");

            if (!Player.immune) {
                if (!noBroadcast) {
                    NetworkHandler.SendPvpHit(physicalDamage, magicalDamage, trueDamage, Player.whoAmI, killer);
                }
                
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
                
                SoundEngine.PlaySound(SoundID.PlayerHit, Player.position);
            }
        }

        public void DealDirectDamageToTarget(int physicalDamage, int magicalDamage, int trueDamage, Player target, bool noBroadcast) {
            AbilityEffectManager.DealPvpDamage(Player, ref physicalDamage, ref magicalDamage, ref trueDamage, target, new DamageSource(Player));
            target.GetModPlayer<MobaPlayer>().TakePvpDamage(physicalDamage, magicalDamage, trueDamage, Player.whoAmI, noBroadcast);
        }
    }
}