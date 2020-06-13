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
    public class SyncTalentsPacket {
    
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32(); 
                CharacterEnum character = (CharacterEnum)reader.ReadInt32();
                bool[,] talents = new bool[7, 4];
                for (int i = 0; i < talents.GetLength(0); i++) {
                    for (int j = 0; j < talents.GetLength(1); j++) {
                        talents[i, j] = reader.ReadBoolean();
                    }
                }
                Write(target, character, talents);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                var plr = Main.player[reader.ReadInt32()].GetModPlayer<MobaPlayer>();
                CharacterEnum character = (CharacterEnum)reader.ReadInt32();
                bool[,] talents = new bool[7, 4];
                for (int i = 0; i < talents.GetLength(0); i++) {
                    for (int j = 0; j < talents.GetLength(1); j++) {
                        talents[i, j] = reader.ReadBoolean();
                    }
                }

                if (plr.CharacterPicked != true) {
                    AssignCharacter(ref plr.MyCharacter, character, plr.player);
                    plr.CharacterPicked = true;
                }
                plr.MyCharacter.talentArray = talents;
            }
        }
    
        public static void Write(int target, CharacterEnum character, bool[,] talents) {
            if (Main.netMode == NetmodeID.Server || Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncTalents);
                packet.Write(target);
                packet.Write((int)character);
                foreach (bool talent in talents) {
                    packet.Write(talent);
                }
                packet.Send();
            }
        }
    }
}