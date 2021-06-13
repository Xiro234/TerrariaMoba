using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Flibnob;

namespace TerrariaMoba.Projectiles.Flibnob {
    public class CullPillar : ModProjectile {

        public float HookRange { get; set; }

        public override void SetDefaults() {
            projectile.width = 30;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 480;

            HookRange = CullTheMeek.HOOK_BASE_RANGE;
        }

        public override void AI() {
            Player player = Main.player[projectile.owner];
            int playerID = -1;
            float closestDist = 30f;
            if (projectile.ai[1] <= 0) {
                projectile.ai[1] = 1;
                for (int i = 0; i < Main.maxPlayers; i++) {
                    float distToPillar = (Main.player[i].Center - projectile.Center).Length() / 16.0f;
                    if (distToPillar <= HookRange && distToPillar < closestDist && i != player.whoAmI && Main.player[i].team != player.team) {
                        playerID = i;
                        closestDist = distToPillar;
                    }
                }
                projectile.ai[0] = playerID;
            }

            if (projectile.ai[0] >= 0) {
                Player hookedPlr = Main.player[(int)projectile.ai[0]];
                if (hookedPlr.active) {
                    float hookedPosX = Math.Abs((hookedPlr.Center.X - projectile.Center.X) / 16.0f);
                    float hookedPosY = Math.Abs((hookedPlr.Center.Y - projectile.Center.Y) / 16.0f);
                    if (hookedPosX > HookRange) {
                        hookedPlr.velocity.X = -hookedPlr.velocity.X * 2f;
                    }
                    if (hookedPosY > HookRange) {
                        hookedPlr.velocity.Y = -hookedPlr.velocity.Y * 2f;
                    }
                }
            }
        }

        public override bool PreDrawExtras(SpriteBatch spriteBatch) {
            Lighting.AddLight(projectile.Center, 0.9f, 0.4f, 0.4f);

            if (projectile.ai[0] >= 0) {
                Player hookedPlr = Main.player[(int)projectile.ai[0]];
                if (!hookedPlr.active) return true;
                float pPosX = hookedPlr.Center.X;
                float pPosY = hookedPlr.Center.Y;
                float gravDir = hookedPlr.gravDir;
                if (gravDir == -1f) pPosY -= 12f;

                Vector2 value = new Vector2(pPosX, pPosY);
                value = hookedPlr.RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
                float projPosX = projectile.position.X + projectile.width * 0.5f - value.X;
                float projPosY = projectile.position.Y + projectile.height * 0.5f - value.Y;
                float rotation2 = (float)Math.Atan2(projPosY, projPosX) - 1.57f;
                bool flag2 = true;

                if (projPosX == 0f && projPosY == 0f) {
                    flag2 = false;
                } else {
                    float projPosXY = (float)Math.Sqrt((projPosX * projPosX + projPosY * projPosY));
                    projPosXY = 12f / projPosXY;
                    projPosX *= projPosXY;
                    projPosY *= projPosXY;
                    value.X -= projPosX;
                    value.Y -= projPosY;
                    projPosX = projectile.position.X + projectile.width * 0.5f - value.X;
                    projPosY = projectile.position.Y + projectile.height * 0.5f - value.Y;
                }

                while (flag2) {
                    float num = 12f;
                    float num2 = (float)Math.Sqrt((projPosX * projPosX + projPosY * projPosY));
                    float num3 = num2;

                    if (float.IsNaN(num2) || float.IsNaN(num3)) {
                        flag2 = false;
                    } else {
                        if (num2 < 20f) {
                            num = num2 - 8f;
                            flag2 = false;
                        }
                        num2 = 12f / num2;
                        projPosX *= num2;
                        projPosY *= num2;
                        value.X += projPosX;
                        value.Y += projPosY;
                        projPosX = projectile.position.X + projectile.width * 0.5f - value.X;
                        projPosY = projectile.position.Y + projectile.height * 0.1f - value.Y;
                        if (num3 > 12f) {
                            float num4 = 0.3f;
                            float num5 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
                            if (num5 > 16f) {
                                num5 = 16f;
                            }
                            num5 = 1f - num5 / 16f;
                            num4 *= num5;
                            num5 = num3 / 80f;
                            if (num5 > 1f) {
                                num5 = 1f;
                            }
                            num4 *= num5;
                            if (num4 < 0f) {
                                num4 = 0f;
                            }
                            num5 = 1f - projectile.localAI[0] / 100f;
                            num4 *= num5;
                            if (projPosY > 0f) {
                                projPosY *= 1f + num4;
                                projPosX *= 1f - num4;
                            } else {
                                num5 = Math.Abs(projectile.velocity.X) / 3f;
                                if (num5 > 1f) {
                                    num5 = 1f;
                                }
                                num5 -= 0.5f;
                                num4 *= num5;
                                if (num4 > 0f) {
                                    num4 *= 2f;
                                }
                                projPosY *= 1f + num4;
                                projPosX *= 1f - num4;
                            }
                        }

                        rotation2 = (float)Math.Atan2(projPosY, projPosX) - 1.57f;

                        Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(114, 115, 116, 100));
                        Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(value.X - Main.screenPosition.X + Main.fishingLineTexture.Width * 0.5f, value.Y - Main.screenPosition.Y + Main.fishingLineTexture.Height * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num)), color2, rotation2, new Vector2(Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                    }
                }

                return false;
            } else {
                return true;
            }
        }
    }
}
