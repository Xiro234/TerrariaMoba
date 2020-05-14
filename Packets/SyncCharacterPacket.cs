using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.Stats;
using static Terraria.ModLoader.ModContent;
using static TerrariaMobaUtils;
    
namespace TerrariaMoba.Packets {
    public class SyncCharacterPacket {
    
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                String name = reader.ReadString();
                bool[,] talents = new bool[7, 4];
                for (int i = 0; i < talents.GetLength(0); i++) {
                    for (int j = 0; j < talents.GetLength(1); j++) {
                        talents[i, j] = reader.ReadBoolean();
                    }
                }
                Write(target, name, talents);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                var plr = Main.player[reader.ReadInt32()].GetModPlayer<TerrariaMobaPlayer_Gameplay>();
                String name = reader.ReadString();
                bool[,] talents = new bool[7, 4];
                for (int i = 0; i < talents.GetLength(0); i++) {
                    for (int j = 0; j < talents.GetLength(1); j++) {
                        talents[i, j] = reader.ReadBoolean();
                    }
                }

                if (plr.CharacterPicked != true) {
                    AssignCharacter(ref plr.MyCharacter, name, plr.player);
                    plr.CharacterPicked = true;
                }
                plr.MyCharacter.talentArray = talents;
            }
        }
    
        public static void Write(int target, String name, bool[,] talents) {
            if (Main.netMode == NetmodeID.Server || Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncCharacter);
                packet.Write(target);
                packet.Write(name);
                foreach (bool talent in talents) {
                    packet.Write(talent);
                }
                packet.Send();
            }
        }
    }
}