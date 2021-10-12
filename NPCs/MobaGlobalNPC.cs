using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.NPCs {
    public class MobaGlobalNPC : GlobalNPC {
        public override bool InstancePerEntity => true;
        public int owner = -1;

        public override void ModifyHitPlayer(NPC NPC, Player target, ref int damage, ref bool crit) {
            //target.GetModPlayer<MobaPlayer>().TakePvpDamage(damage, target.whoAmI, owner, true);
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                //new PvpHitPacket(null, null, (byte) target.whoAmI, damage, (byte) owner).Send();
                //TODO - Fix NPC hits
            }
        }

        public override bool PreKill(NPC NPC) {
            return false;
        }

        public override bool? DrawHealthBar(NPC NPC, byte hbPosition, ref float scale, ref Vector2 position) {
            return false;
        }

        public override bool CanHitPlayer(NPC NPC, Player target, ref int cooldownSlot) {
            if(NPC.GetGlobalNPC<MobaGlobalNPC>().owner < 0 || NPC.GetGlobalNPC<MobaGlobalNPC>().owner > 255) {
                return base.CanHitPlayer(NPC, target, ref cooldownSlot);
            }
            
            if (target.team == Main.player[NPC.GetGlobalNPC<MobaGlobalNPC>().owner].team) {
                return false;
            }

            return base.CanHitPlayer(NPC, target, ref cooldownSlot);
        }

        public override bool? CanBeHitByItem(NPC NPC, Player Player, Item item) {
            if (owner < 0 || owner > 255) {
                return base.CanBeHitByItem(NPC, Player, item);
            }
            
            if (Player.team == Main.player[owner].team) {
                return false;
            }

            return CanBeHitByItem(NPC, Player, item);
        }

        public override bool? CanBeHitByProjectile(NPC NPC, Projectile projectile) {
            if (owner < 0 || owner > 255) {
                return base.CanBeHitByProjectile(NPC, projectile);
            }
            
            if (Main.player[projectile.owner].team == Main.player[owner].team) {
                return false;
            }
            
            return base.CanBeHitByProjectile(NPC, projectile);
        }
        
        public override void PostDraw(NPC NPC, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
            if (!NPC.dontTakeDamage) {
                Color gradA;
                Color gradB;
                if (owner < 0 || owner > 255 || Main.player[owner].team != Main.LocalPlayer.team) {
                    gradA = Color.DarkRed;
                    gradB = Color.Red;
                }
                else {
                    gradA = Color.DarkGreen;
                    gradB = Color.Lime;
                }

                Texture2D healthBar = ModContent.Request<Texture2D>("Textures/NpcHealthBar").Value;
                Vector2 barPos = new Vector2(NPC.Top.X - Main.screenPosition.X - 31,
                    NPC.Top.Y - screenPos.Y - 28);
                spriteBatch.Draw(healthBar, barPos, drawColor);

                float quotient = Utils.Clamp((float) NPC.life / NPC.lifeMax, 0f, 1f);

                int left = 0;
                int right = healthBar.Width;
                int top = 0;
                int bottom = healthBar.Height;
                int steps = (int) ((right - left - 4) * quotient);
                int i = 0;
                for (i = 0; i < steps; i++) {
                    float percent = (float) i / (right - left);
                    Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, new Vector2(barPos.X + i + 2, barPos.Y + 2),
                        new Rectangle(0, 0, 1, 4),
                        Color.Lerp(gradA, gradB, percent));
                }
            }
        }
    }
}