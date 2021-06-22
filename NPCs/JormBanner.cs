using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.NPCs {
    public class JormBanner : ModNPC {

        public override void SetDefaults() {
            npc.width = 102;
            npc.height = 126;
            npc.aiStyle = -1;
            npc.lifeMax = 10;
            npc.damage = 0;
            npc.defense = 9999;
            npc.dontTakeDamage = false;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit56;
            npc.DeathSound = SoundID.NPCDeath56;
        }

        public override void AI() {
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float distToBanner = (Main.player[i].Center - npc.Center).Length() / 16.0f;
                if (plr.active) {
                    if (plr.team == Main.player[npc.GetGlobalNPC<MobaGlobalNPC>().owner].team && distToBanner < npc.ai[0]) {
                        Main.NewText("In range, buffing player.");
                    }
                    else {
                        Main.NewText("Out of range!");
                    }
                }
            }
        }
    }
}