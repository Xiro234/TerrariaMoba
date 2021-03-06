using System.Collections.Generic;
using Terraria.ModLoader;
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
using TerrariaMoba.Interfaces;
using TerrariaMoba.Packets.General;
using TerrariaMoba.Stats;
using TerrariaMoba.UI;
using Effect = TerrariaMoba.Effects.Effect;

namespace TerrariaMoba.Players {
    public class MobaPlayer : ModPlayer {
        //General
        public Character MyCharacter;
        public CharacterIdentity selectedCharacter;

        public bool CharacterPicked {
            get => selectedCharacter != CharacterIdentity.Base;
        }
        
        
        public int PlayerLastHurt = -1;
        public bool Silenced = false;
        public bool Weakened = false;
        public bool IsChanneling = false;
        public bool InProgress = false;
        public int GameTime = 0;

        public SylviaEffects SylviaEffects;
        public MarieEffects MarieEffects;
        public FlibnobEffects FlibnobEffects;

        public SylviaStats SylviaStats;
        public MarieStats MarieStats;
        public FlibnobStats FlibnobStats;
        public OsteoStats OsteoStats;

        //Custom Stats
        public float percentThorns = 0f;
        //public int shield = 0;

        public int maxLife = 0;
        public int bonusLife = 0;
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
        public float movementSpeed = 1f;

        public int ultTimer = -1;
        public Ability TestAbility;
        public List<Ability> TestAbilities;

        public List<Effect> effectList;
        
        public override void Initialize() {
            if (!Main.dedServ) {
                selectedCharacter = CharacterIdentity.Base;
                SylviaEffects = new SylviaEffects();
                MarieEffects = new MarieEffects();
                FlibnobEffects = new FlibnobEffects();
                SylviaStats = new SylviaStats();
                MarieStats = new MarieStats();
                FlibnobStats = new FlibnobStats();
                OsteoStats = new OsteoStats();

                effectList = new List<Effect>();
            }
        }

        public override void OnEnterWorld(Player player) {
            TerrariaMoba.Instance.MobaBar = null;
            TerrariaMoba.Instance.MobaBar = new MobaBar();
            TerrariaMoba.Instance.HideBar();
            
            TestAbility = new TestKillAbility();
            TestAbilities = new List<Ability>();
            TestAbilities.Add(new TestKillAbility());
            TestAbilities.Add(new TestShootAbility());
        }

        public override void OnRespawn(Player player) {
            player.statLife = player.statLifeMax;
        }

        public override void ResetEffects() {
            maxLife = 0;
            bonusLife = 0;
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
                TestAbility.OnCast();
                
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

            if (TerrariaMoba.OpenCharacterSelect.JustPressed) {
                if (TerrariaMoba.Instance.SelectInterface.CurrentState == null && !CharacterPicked) {
                    TerrariaMoba.Instance.ShowSelect();
                }
                else {
                    TerrariaMoba.Instance.HideSelect();
                }
            }
            */
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage,
            ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if (pvp) {
                return false;
            }
            return true;
        }

        public override void PreUpdate() {
            if (!Main.dedServ) {
                if (CharacterPicked && InProgress) {
                }
            }
        }

        public override void PostUpdateEquips() {
            if (CharacterPicked && InProgress) {
                MyCharacter.UpdateBaseStats();
                movementSpeed = 1f;
                
                /*
                for (int i = effectList.Count - 1; i >= 0; i--) {
                    var effect = effectList[i];
                    effect.duration--;
                    if (effect.duration <= 0) {
                        effect.FallOff(player);
                        effectList.RemoveAt(i);
                    }
                    else {
                        effect.Tick(player);
                    }
                }
                */
                
                player.statLifeMax2 = maxLife + bonusLife;

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
                
                foreach (Ability ability in MyCharacter.Abilities.Where(ability => ability.IsActive)) {
                    ability.WhileActive();
                }

                foreach (Ability ability in MyCharacter.Abilities.Where(ability => ability.CooldownTimer > 0)) {
                    ability.CooldownTimer--;
                }
            }
        }

        public override void PostUpdate() {
            base.PostUpdate();
        }

        public override void PostUpdateBuffs() {
            if (CharacterPicked && InProgress) {
            }

            if (MarieEffects.LacusianBlessing) {
                player.statDefense += 12;
                player.lifeRegen += 8;
                player.allDamageMult += (float)0.16;
            }
        }

        public override void PostUpdateRunSpeeds() {
            if (CharacterPicked && InProgress) {
            }

            player.maxRunSpeed *= movementSpeed;
            player.accRunSpeed *= movementSpeed;
            player.moveSpeed *= movementSpeed;
        }
        
        public override void NaturalLifeRegen(ref float regen) {
            regen *= 0;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (Main.netMode == NetmodeID.MultiplayerClient && pvp) {
                TerrariaMobaUtils.MobaKill(damageSource.SourcePlayerIndex, player.whoAmI);
            }
            else {
                //TerrariaMobaUtils.MobaKill(PlayerLastHurt, player.whoAmI);
                AbilityEffectManager.Kill(player, damage, hitDirection, pvp, damageSource);
            }
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            if (InProgress) {
                EditDamage(ref damage);
                if (Main.netMode == NetmodeID.SinglePlayer) {
                    target.GetModPlayer<MobaPlayer>().DamageOverride(damage, target, player.whoAmI, true);
                }
                else {
                    PvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
                }
            }
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
            if (InProgress) {
                EditDamage(ref damage);
                if (Main.netMode == NetmodeID.SinglePlayer) {
                    target.GetModPlayer<MobaPlayer>().DamageOverride(damage, target, player.whoAmI, true);
                }
                else {
                    PvpHitPacket.Write(target.whoAmI, damage, player.whoAmI, true);
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection) {
            if (InProgress) {
                EditDamage(ref damage);
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
            Texture2D healthBar = TerrariaMoba.Instance.GetTexture("Textures/PlayerHealthBar");
            Vector2 barPos = new Vector2(player.Top.X - Main.screenPosition.X - (healthBar.Width/2),
                player.Top.Y - Main.screenPosition.Y - 28);
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
                    new Rectangle(0, 0, 1, 10),
                    Color.Lerp(gradA, gradB, percent));

                if (i % stepsPerHundred == 0 && i != 0) {
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
                }
            }
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {
            if (CharacterPicked && InProgress) {

            }

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
            PlayerLayer MiscEffectsFront = MobaLayers.MiscEffectsFront;
            MiscEffectsFront.visible = true;
            
            PlayerLayer MiscEffectsBack = MobaLayers.MiscEffectsBack;
            MiscEffectsBack.visible = true;
            
            layers.Add(MiscEffectsFront);
            layers.Add(MiscEffectsBack);
            
            if (CharacterPicked && InProgress) {
            }
        }
        
        public override void SetControls() {
            if (IsChanneling || SylviaEffects.EnsnaringVines) {
                player.controlRight = false;
                player.controlLeft = false;
                player.controlJump = false;
                player.controlUp = false;
                player.controlDown = false;
            }

            if (CharacterPicked && InProgress) {
                
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
                
                if (percentThorns > 0f && sendThorns) {
                    if (Main.netMode == NetmodeID.SinglePlayer) {
                        target.GetModPlayer<MobaPlayer>().DamageOverride((int)(damage * percentThorns), Main.player[killer], target.whoAmI, false);
                    }
                    else {
                        PvpHitPacket.Write(killer, (int)(damage * percentThorns), target.whoAmI, false);
                    }
                }

                int otherArmor = target.GetModPlayer<MobaPlayer>().armor;
                damage *= ((100 - otherArmor) / 100);
                target.statLife -= damage;

                CombatText.NewText(target.Hitbox, Color.OrangeRed, damage);
                
                if (target.statLife <= 0) {
                    target.KillMe(PlayerDeathReason.ByPlayer(PlayerLastHurt), damage, 1, true);
                }
                
                if (sendThorns) {
                    Main.PlaySound(1, target.position);
                }
            }
        }

        public void AddEffect(Effect effect) {
            effectList.Add(effect);
            if (Main.LocalPlayer == player) {
                //EffectPacket.Write(effect);
            }
        }
        
        public void StartGame() {
            MyCharacter.SetCharacter();
            InProgress = true;
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