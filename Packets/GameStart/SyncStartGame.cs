/*using System.IO;
using Terraria;
using TerrariaMoba.Players;
using WebmilioCommons.Networking;
using WebmilioCommons.Networking.Packets;

namespace TerrariaMoba.Packets.GameStart {
    public class SyncStartGame : ModPlayerNetworkPacket<MobaPlayer> {
        public override NetworkPacketBehavior Behavior { get => NetworkPacketBehavior.SendToAllClients; }

        protected override bool MidReceive(BinaryReader reader, int fromWho) {
            for (int i = 0; i < Main.maxPlayers; i++) {
                if (Main.player[i] != null && Main.player[i].active) {
                    var plr = Main.player[i].GetModPlayer<MobaPlayer>();
                    plr.StartGame();
                }
            }
            return true;
        }
    }
}*/