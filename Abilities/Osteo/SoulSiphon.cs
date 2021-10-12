using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Osteo {
    public class SoulSiphon : Ability {
        public SoulSiphon() : base("Soul Siphon", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Osteo/OsteoUltimateTwo").Value; }
    }
}

/*using System;
using System.Collections.Generic;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.NPCs;
using TerrariaMoba.Players;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Osteo {
    [Serializable]
    public class SoulSiphon : Ability {
        private List<Projectile> soulList;
        private List<Vector2> soulPositions;

        public SoulSiphon(Player myPlayer) : base(myPlayer) {
            Name = "Soul Siphon";
            Icon = ModContent.Request<Texture2D>("Textures/Osteo/OsteoUltimateTwo").Value;
            soulList = new List<Projectile>();
            soulPositions = new List<Vector2>();
        }


        public override void OnCast() {
            if (soulList.Count == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    var proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this),User.Center, Vector2.Zero,
                        ModContent.ProjectileType<OsteoSoul"), 0, 0, User.whoAmI);
                    soulList.Add(proj);
                }
                soulPositions.Add(User.Center);
            }
            else {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Projectile consumeSoul = null;
                    foreach (var soul in soulList) {
                        if (Vector2.Distance(soul.position, User.Center) < 16 * 16) {
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
                    if (Vector2.Distance(soul, User.Center) < 16 * 16) {
                        consumeSoulNullable = soul;
                        break;
                    }
                }
                
                if (consumeSoulNullable != null) {
                    Vector2 consumeSoul = consumeSoulNullable.GetValueOrDefault();
                    soulPositions.Remove(consumeSoul);
                    float distance = Vector2.Distance(consumeSoul, User.Center);

                    for (float i = 0f; i < distance; i ++) {
                        Vector2 position = Vector2.Lerp(consumeSoul, User.Center, i / distance);
                        var dust = Dust.NewDustPerfect(position, 15, Vector2.Zero, 0, Color.SteelBlue, 1f);
                        dust.noGravity = true;
                        dust.noLight = true;
                    }

                    int maxSkeles = 6;
                    for (int i = 0; i < maxSkeles; i++) {
                        if (i < maxSkeles / 2) {
                            Vector2 position = BaseAI.Trace(
                                new Vector2(User.Center.X + ((96) * User.direction) + (i * 32),
                                    User.Center.Y - 64),
                                new Vector2(User.Center.X, Main.bottomWorld), new Vector2(-1, -1), -1, false, true);

                            int npc = NPC.NewNPC((int) position.X, (int) position.Y, NPCType<OsteoSkeleton>());

                            Main.npc[npc].GetGlobalNPC<MobaGlobalNPC>().owner = User.whoAmI;
                            Main.npc[npc].direction = User.direction;
                            (User.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList.Add(
                                Main.npc[npc]);
                        }
                        else {
                            Vector2 position = BaseAI.Trace(
                                new Vector2(User.Center.X + ((96) * -User.direction) - ((i - maxSkeles / 2) * 32),
                                    User.Center.Y - 64),
                                new Vector2(User.Center.X, Main.bottomWorld), new Vector2(-1, -1), -1, false, true);

                            int npc = NPC.NewNPC((int) position.X, (int) position.Y, NPCType<OsteoSkeleton>());

                            Main.npc[npc].GetGlobalNPC<MobaGlobalNPC>().owner = User.whoAmI;
                            Main.npc[npc].direction = -User.direction;
                            (User.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList.Add(
                                Main.npc[npc]);
                        }
                    }
                    IsActive = true;
                }
            }
        }

        public override void WhileActive() {
            if (Timer == 360) {
                TimeOut();
            }

            //some buff here
            
            Timer++;
        }

        public override void TimeOut() {
            Timer = 0;
            IsActive = false;
        }
    }
}*/