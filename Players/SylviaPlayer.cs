using System.Collections.Generic;
using TerrariaMoba;
using TerrariaMoba.Stats;
using TerrariaMoba.Packets;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Players {
    public class SylviaPlayer : ModPlayer {
        //Linking
        public bool IsSylvia = false;
        
        //SylviaStats
        public SylviaStats MySylviaStats;
        public bool VerdantFury = false;
        public bool JunglesWrath = false;
        public bool EnsnaringVines = false;
        public bool IsPhasing = false;
        public bool SylviaUlt1 = false;
        public int SylviaUlt1Timer = 0;
        public int JunglesWrathCount = 1;
        public int NumberJavelins = 0;

        public override void Initialize() {
            MySylviaStats = new SylviaStats();
        }

        public override void ResetEffects() {
            VerdantFury = false;
            JunglesWrath = false;
            EnsnaringVines = false;
        }

        public override void PreUpdate() {
            if (IsSylvia) {
                if (SylviaUlt1Timer > 0) {
                    SylviaUlt1Timer--;
                    if (SylviaUlt1Timer == 0) {
                        SylviaUlt1 = false;
                        NumberJavelins = 0;
                        //SyncSylviaUlt1Packet.Write(player.whoAmI, SylviaUlt1);
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
                JunglesWrathCount = 1;
            }

            if (IsSylvia) {
                //IsPhasing
                if (IsPhasing) {
                    player.immune = true;
                    player.immuneTime = 1;
                }
            }
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type,
            ref int damage, ref float knockBack) {
            if (IsSylvia) {
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
                        //SyncSylviaUlt1Packet.Write(player.whoAmI, SylviaUlt1);
                    }

                    return false;
                }
            }
            return true;
        }

        public override float UseTimeMultiplier(Item item) {
            if (IsSylvia) {
                //VerdantFury
                if (VerdantFury && item.type == mod.ItemType("SylviaBow")) {
                    return MySylviaStats.GetVerdantFuryIncrease();
                }
            }
            return 1f;
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
            SylviaPlayer modPlayer = drawPlayer.GetModPlayer<SylviaPlayer>();

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
            
            //IsPhasing
            if (IsSylvia) {
                if (IsPhasing) {
                    foreach (PlayerLayer layer in layers) {
                        layer.visible = false;
                    }
                }
            }
        }
        
        public override void PreUpdateMovement() {
            if (IsSylvia) {
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
        }

        public override void PostUpdateRunSpeeds() {
            if (IsSylvia) {
                MobaPlayer modPlayer = player.GetModPlayer<MobaPlayer>();
                float moveSpeedAdd = 1f;
                //Sylvia Talent [0,0]: Nature's Calling
                if (modPlayer.MyCharacter.talentArray[0, 0]) {
                    moveSpeedAdd += 0.36f;
                }

                //Sylvia Talent [1,0]: Swift Thistle
                if (modPlayer.MyCharacter.talentArray[1, 0] && VerdantFury) {
                    moveSpeedAdd += MySylviaStats.GetVerdantFuryIncrease() - 1f;
                }

                player.moveSpeed *= moveSpeedAdd;
                player.maxRunSpeed *= moveSpeedAdd;
                player.accRunSpeed *= moveSpeedAdd;
            }
        }
        
        public override void SetControls() {
            //EnsnaringVines
            if (EnsnaringVines) {
                player.controlRight = false;
                player.controlLeft = false;
                player.controlJump = false;
                player.controlUp = false;
                player.controlDown = false;
            }
        }
        
        public override void PostUpdateEquips() {
            if (IsSylvia) {
                MobaPlayer mobaPlayer = player.GetModPlayer<MobaPlayer>();

                //Sylvia Talent [0,1]: Graceful Leap
                if (mobaPlayer.MyCharacter.talentArray[0, 1]) {
                    player.doubleJumpCloud = true; //doesn't work, needs to sync somewhere idk
                }

                //Sylvia Talent [0,2]: Thorns Embrace
                if (mobaPlayer.MyCharacter.talentArray[0, 2]) {
                    player.statDefense += 6;
                    mobaPlayer.PercentThorns += 0.15f;
                }
            }
        }
        
        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            if (IsSylvia) {
                //Jungles Wrath
                if (proj.ranged) {
                    target.AddBuff(BuffType<Buffs.JunglesWrath>(), 240, false);
                }
            }
        }
    }
}