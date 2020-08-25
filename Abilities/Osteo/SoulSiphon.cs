using System.Collections.Generic;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.NPCs;
using TerrariaMoba.Players;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Osteo {
    public class SoulSiphon : Ability {
        private List<Projectile> soulList;
        private List<Vector2> soulPositions;

        public SoulSiphon(Player myPlayer) : base(myPlayer) {
            Name = "Soul Siphon";
            soulList = new List<Projectile>();
            soulPositions = new List<Vector2>();
        }


        public override void Cast() {
            if (soulList.Count == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                    var proj = Projectile.NewProjectileDirect(player.Center, Vector2.Zero,
                        TerrariaMoba.Instance.ProjectileType("OsteoSoul"), 0, 0, player.whoAmI);
                    soulList.Add(proj);
                }
                soulPositions.Add(player.Center);
            }
            else {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                    Projectile consumeSoul = null;
                    foreach (var soul in soulList) {
                        if (Vector2.Distance(soul.position, player.Center) < 16 * 16) {
                            consumeSoul = soul;
                            break;
                        }
                    }

                    if (consumeSoul != null) {
                        consumeSoul.Kill();
                        soulList.Remove(consumeSoul);
                    }
                }

                Vector2? consumeSoulNullable = null;
                foreach (Vector2 soul in soulPositions) {
                    if (Vector2.Distance(soul, player.Center) < 16 * 16) {
                        consumeSoulNullable = soul;
                        break;
                    }
                }
                
                if (consumeSoulNullable != null) {
                    Vector2 consumeSoul = consumeSoulNullable.GetValueOrDefault();
                    soulPositions.Remove(consumeSoul);
                    float distance = Vector2.Distance(consumeSoul, player.Center);

                    for (float i = 0f; i < distance; i ++) {
                        Vector2 position = Vector2.Lerp(consumeSoul, player.Center, i / distance);
                        var dust = Dust.NewDustPerfect(position, 15, Vector2.Zero, 0, Color.SteelBlue, 1f);
                        dust.noGravity = true;
                        dust.noLight = true;
                    }

                    int maxSkeles = 6;
                    for (int i = 0; i < maxSkeles; i++) {
                        if (i < maxSkeles / 2) {
                            Vector2 position = BaseAI.Trace(
                                new Vector2(player.Center.X + ((96) * player.direction) + (i * 32),
                                    player.Center.Y - 64),
                                new Vector2(player.Center.X, Main.bottomWorld), new Vector2(-1, -1), -1, false, true);

                            int npc = NPC.NewNPC((int) position.X, (int) position.Y, NPCType<OsteoSkeleton>());

                            Main.npc[npc].GetGlobalNPC<MobaGlobalNPC>().owner = player.whoAmI;
                            Main.npc[npc].direction = player.direction;
                            (player.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList.Add(
                                Main.npc[npc]);
                        }
                        else {
                            Vector2 position = BaseAI.Trace(
                                new Vector2(player.Center.X + ((96) * -player.direction) - ((i - maxSkeles / 2) * 32),
                                    player.Center.Y - 64),
                                new Vector2(player.Center.X, Main.bottomWorld), new Vector2(-1, -1), -1, false, true);

                            int npc = NPC.NewNPC((int) position.X, (int) position.Y, NPCType<OsteoSkeleton>());

                            Main.npc[npc].GetGlobalNPC<MobaGlobalNPC>().owner = player.whoAmI;
                            Main.npc[npc].direction = -player.direction;
                            (player.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList.Add(
                                Main.npc[npc]);
                        }
                    }
                    IsActive = true;
                }
            }
        }

        public override void Using() {
            if (Timer == 360) {
                End();
            }

            //some buff here
            
            Timer++;
        }

        public override void End() {
            Timer = 0;
            IsActive = false;
        }
    }
}