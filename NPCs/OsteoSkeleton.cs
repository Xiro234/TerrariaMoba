using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static TerrariaMoba.TerrariaMobaUtils;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace TerrariaMoba.NPCs {
    public class OsteoSkeleton : ModNPC {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 22;
        }

        public override void SetDefaults() {
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = -1;
            AnimationType = NPCID.Skeleton;
            NPC.lifeMax = 20;
            NPC.damage = 10;
            NPC.defense = 0;
            NPC.dontTakeDamage = true;
            NPC.knockBackResist = 1f;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
        }
        
        private const int groundTime = 120;
        private int groundTimer = 0;
        private bool doEffect = false;

        public override void AI() {
            //TODO - Fix BaseAI code for Osteo's skeletons.
            if (groundTimer > groundTime) {
                if (!NPC.HasValidTarget) {
                    TargetClosestEnemy(NPC);
                }
                else {
                    //BaseAI.SetTarget(NPC, NPC.target);
                    NPC.FaceTarget();
                }

                //BaseAI.AIZombie(NPC, ref NPC.ai, false, false, 0, 0.07f, 3f, 8, 4, 60,
                    //false);
                NPC.dontTakeDamage = false;
                NPC.knockBackResist = 0f;
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
                    Dust.NewDust(NPC.BottomLeft, NPC.width, 0, DustID.Stone);
                    SoundEngine.PlaySound(SoundID.Dig, NPC.position);
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
                NPC.frame.Y = (frameHeight * 15) + frameHeight * (groundTimer / (groundTime / 7));
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