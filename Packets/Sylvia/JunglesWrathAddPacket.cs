using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Buffs;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Packets.General {
    public class JunglesWrathAddPacket {
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                int stacks = reader.ReadInt32();
                bool doEffects = reader.ReadBoolean();
                Write(target, stacks, doEffects);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                int target = reader.ReadInt32();
                int stacks = reader.ReadInt32();
                bool doEffects = reader.ReadBoolean();
                
                Main.player[target].AddBuff(BuffType<JunglesWrathBuff>(), 240, true);
                var mobaPlayer = Main.player[target].GetModPlayer<MobaPlayer>();

                if (doEffects) {
                    Main.PlaySound(SoundID.Grass, mobaPlayer.player.position);
                }
                mobaPlayer.SylviaEffects.JunglesWrathCount = stacks;
            }
        }
    
        public static void Write(int target, int stacks, bool doEffects) {
            if (Main.netMode == NetmodeID.Server || Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncJunglesWrathAdd);
                packet.Write(target);
                packet.Write(stacks);
                packet.Write(doEffects);
                packet.Send();
            }
        }
    }
}