using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Packets {
    public class SyncAbilitiesPacket {
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int index = reader.ReadInt32();
                int fromWho = reader.ReadInt32();
                Write(index, fromWho);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                int index = reader.ReadInt32();
                int fromWho = reader.ReadInt32();
                var player = Main.player[fromWho].GetModPlayer<MobaPlayer>();

                player.MyCharacter.abilities[index].Cast();
            }
        }

        public static void Write(int index, int fromWho) {
            if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncAbilities);
                packet.Write(index);
                packet.Write(fromWho);
                packet.Send();
            }
        }
    }
}