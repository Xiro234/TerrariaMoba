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
    public class SyncJunglesWrathPacket {
    
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                bool add = reader.ReadBoolean();
                Write(target, add);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                //var plr = Main.LocalPlayer.GetModPlayer<SylviaPlayer>();
                bool add = reader.ReadBoolean();
                if (add) {
                    //plr.JunglesWrathCount++;
                }

                int index = Main.LocalPlayer.FindBuffIndex(BuffType<Buffs.JunglesWrath>());
                //Main.LocalPlayer.buffTime[index] = plr.MySylviaStats.GetJunglesWrathTime();
            }
        }
    
        public static void Write(int target, bool add) {
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncJunglesWrath);
                packet.Write(target);
                packet.Write(add);
                packet.Send();
            }
            else if (Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte)Message.SyncJunglesWrath);
                packet.Write(add);
                packet.Send(target);
            }
        }
    }
}