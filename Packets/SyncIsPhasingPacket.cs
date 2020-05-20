using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Buffs;
using TerrariaMoba.Players;
using TerrariaMoba.Stats;
using static Terraria.ModLoader.ModContent;
    
namespace TerrariaMoba.Packets {
    public class SyncSylviaIsPhasingPacket {
    
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                bool IsUlting = reader.ReadBoolean();
                Write(target, IsUlting);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                var plr = Main.player[reader.ReadInt32()].GetModPlayer<SylviaPlayer>();
                bool IsUlting = reader.ReadBoolean();
                plr.SylviaUlt1 = IsUlting;
            }
        }
    
        public static void Write(int target, bool IsUlting) {
            ModPacket packet = TerrariaMoba.Instance.GetPacket();
            packet.Write((byte) Message.SyncSylviaUlt1);
            packet.Write(target);
            packet.Write(IsUlting);
            packet.Send();
        }
    }
}