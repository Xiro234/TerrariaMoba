using System.IO;
using Terraria;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using WebmilioCommons.Networking;
using WebmilioCommons.Networking.Packets;

namespace TerrariaMoba.Packets.GameStart {
    public class SyncCharacterSelection : ModPlayerNetworkPacket<MobaPlayer> {
        public override NetworkPacketBehavior Behavior { get => NetworkPacketBehavior.SendToAllClients; }

        public byte selectedCharacter {
            get => (byte)ModPlayer.selectedCharacter;
            set => ModPlayer.selectedCharacter = (CharacterIdentity)value;
        }

        protected override bool PostReceive(BinaryReader reader, int fromWho) {
            if (Main.LocalPlayer != ModPlayer.player) {
                TerrariaMobaUtils.AssignCharacter(ref ModPlayer.MyCharacter, ModPlayer.selectedCharacter,
                    ModPlayer.player);
            }
            return true;
        }
    }
}