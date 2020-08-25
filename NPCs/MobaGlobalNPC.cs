using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Packets;
using TerrariaMoba.Players;

namespace TerrariaMoba.NPCs {
    public class MobaGlobalNPC : GlobalNPC {
        public override bool InstancePerEntity => true;
        public int owner = -1;

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit) {
            if (Main.netMode == NetmodeID.SinglePlayer) {
                target.GetModPlayer<MobaPlayer>().DamageOverride(damage, target, owner, true);
            }
            else {
                PvpHitPacket.Write(target.whoAmI, damage, owner, true);
            }
        }

        public override bool PreNPCLoot(NPC npc) {
            return false;
        }

        public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position) {
            return false;
        }

        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot) {
            if(npc.GetGlobalNPC<MobaGlobalNPC>().owner < 0 || npc.GetGlobalNPC<MobaGlobalNPC>().owner > 255) {
                return base.CanHitPlayer(npc, target, ref cooldownSlot);
            }
            
            if (target.team == Main.player[npc.GetGlobalNPC<MobaGlobalNPC>().owner].team) {
                return false;
            }

            return base.CanHitPlayer(npc, target, ref cooldownSlot);
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item) {
            if (owner < 0 || owner > 255) {
                return base.CanBeHitByItem(npc, player, item);
            }
            
            if (player.team == Main.player[owner].team) {
                return false;
            }

            return CanBeHitByItem(npc, player, item);
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile) {
            if (owner < 0 || owner > 255) {
                return base.CanBeHitByProjectile(npc, projectile);
            }
            
            if (Main.player[projectile.owner].team == Main.player[owner].team) {
                return false;
            }
            
            return base.CanBeHitByProjectile(npc, projectile);
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor) {
            if (!npc.dontTakeDamage) {
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

                Texture2D healthBar = TerrariaMoba.Instance.GetTexture("Textures/NpcHealthBar");
                Vector2 barPos = new Vector2(npc.Top.X - Main.screenPosition.X - 31,
                    npc.Top.Y - Main.screenPosition.Y - 28);
                spriteBatch.Draw(healthBar, barPos, drawColor);

                float quotient = Utils.Clamp((float) npc.life / npc.lifeMax, 0f, 1f);

                int left = 0;
                int right = healthBar.Width;
                int top = 0;
                int bottom = healthBar.Height;
                int steps = (int) ((right - left - 4) * quotient);
                int i = 0;
                for (i = 0; i < steps; i++) {
                    float percent = (float) i / (right - left);
                    spriteBatch.Draw(Main.magicPixel, new Vector2(barPos.X + i + 2, barPos.Y + 2),
                        new Rectangle(0, 0, 1, 4),
                        Color.Lerp(gradA, gradB, percent));
                }
            }
        }
    }
}