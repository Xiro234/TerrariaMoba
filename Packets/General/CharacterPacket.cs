using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Packets {
    public class CharacterPacket {
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                Write(target);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                int target = reader.ReadInt32();
                Main.player[target].GetModPlayer<MobaPlayer>().MyCharacter.ReadCharacter(reader);
            }
        }
    
        public static void Write(int target) {
            if (Main.netMode == NetmodeID.Server || Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket(512);
                packet.Write((byte) Message.SyncCharacter);
                Main.player[target].GetModPlayer<MobaPlayer>().MyCharacter.WriteCharacter(packet);
                packet.Send();
            }
        }
    }
}