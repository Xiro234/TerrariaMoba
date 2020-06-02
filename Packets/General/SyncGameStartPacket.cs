using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Packets {
    public class SyncGameStartPacket {
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                Write();
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                var plr = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
                plr.StartGame();
            }
        }

        public static void Write() {
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncGameStart);
                packet.Send();
            }
            else if (Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte)Message.SyncGameStart);
                packet.Send();
            }
        }
    }
}