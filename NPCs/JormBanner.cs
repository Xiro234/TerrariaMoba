using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Jorm;

namespace TerrariaMoba.NPCs {
    public class JormBanner : ModNPC {

        public override void SetDefaults() {
            NPC.width = 102;
            NPC.height = 126;
            NPC.aiStyle = -1;
            NPC.lifeMax = 10;
            NPC.damage = 0;
            NPC.defense = 9999;
            NPC.dontTakeDamage = false;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit56;
            NPC.DeathSound = SoundID.NPCDeath56;
        }

        public override void AI() {
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float distToBanner = (Main.player[i].Center - NPC.Center).Length() / 16.0f;
                if (plr.active && plr.team == Main.player[NPC.GetGlobalNPC<MobaGlobalNPC>().owner].team) {
                    if (distToBanner < NPC.ai[0]) {
                        StatusEffectManager.AddEffect(plr, new VexillumImmortalisEffect(), true);
                    } else {
                        // needs to be tested
                    }
                }
            }
        }
    }
}