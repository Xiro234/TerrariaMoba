using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Packets {
    public class SyncAbilityValues {
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int index = reader.ReadInt32();
                int fromWho = reader.ReadInt32();
                int length = reader.ReadInt32();
                byte[] abilitySpecific = reader.ReadBytes(length);
                Write(index, fromWho, length, abilitySpecific);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                int index = reader.ReadInt32();
                int fromWho = reader.ReadInt32();
                int length = reader.ReadInt32();
                byte[] abilitySpecific = reader.ReadBytes(length);

                var mobaPlayer = Main.player[fromWho].GetModPlayer<MobaPlayer>();
                mobaPlayer.MyCharacter.abilities[index].ReadAbility(new MemoryStream(abilitySpecific));
            }
        }

        public static void Write(int index, int fromWho, int length,  byte[] abilitySpecific) {
            if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncAbilityValues);
                packet.Write(index);
                packet.Write(fromWho);
                packet.Write(length);
                packet.Write(abilitySpecific);
                packet.Send();
            }
        }
    }
}