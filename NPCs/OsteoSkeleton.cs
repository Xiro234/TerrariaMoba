using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static TerrariaMobaUtils;
using BaseMod;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.NPCs {
    public class OsteoSkeleton : ModNPC {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[npc.type] = 22;
        }

        public override void SetDefaults() {
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = -1;
            animationType = NPCID.Skeleton;
            npc.lifeMax = 20;
            npc.damage = 10;
            npc.defense = 0;
            npc.dontTakeDamage = true;
            npc.knockBackResist = 1f;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath2;
        }
        
        private const int groundTime = 120;
        private int groundTimer = 0;
        private bool doEffect = false;

        public override void AI() {
            if (groundTimer > groundTime) {
                if (!npc.HasValidTarget) {
                    TargetClosestEnemy(npc);
                }
                else {
                    BaseAI.SetTarget(npc, npc.target);
                    npc.FaceTarget();
                }

                BaseAI.AIZombie(npc, ref npc.ai, false, false, 0, 0.07f, 3f, 8, 4, 60,
                    false);
                npc.dontTakeDamage = false;
                npc.knockBackResist = 0f;
            }
            else {
                int timerBefore = groundTimer;
                groundTimer++;
                
                if((groundTimer / (groundTime / 7)) > (timerBefore / (groundTime / 7))) {
                    doEffect = true;
                }
                else {
                    doEffect = false;
                }
            }
        }

        public override void DrawEffects(ref Color drawColor) {
            if (groundTimer <= groundTime && doEffect) {
                for (int i = 0; i < 5; i++) {
                    Dust.NewDust(npc.BottomLeft, npc.width, 0, 1);
                    Main.PlaySound(SoundID.Dig, npc.position);
                }
            }
        }
        
        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(groundTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            groundTimer = reader.ReadInt32();
        }

        public override void FindFrame(int frameHeight) {
            base.FindFrame(frameHeight);
            if (groundTimer < groundTime - 2) { //prevents blinking
                npc.frame.Y = (frameHeight * 15) + frameHeight * (groundTimer / (groundTime / 7));
            }
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) {
            if (groundTimer < groundTime) {
                return false;
            }

            return base.CanHitPlayer(target, ref cooldownSlot);
        }
    }
}