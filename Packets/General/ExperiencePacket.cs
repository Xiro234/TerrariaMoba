using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Packets {
    public class ExperiencePacket {

        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                float xp = reader.ReadSingle();
                Write(xp, target);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                var plr = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
                int xp = (int)reader.ReadSingle();
                if (plr.CharacterPicked) {
                    plr.MyCharacter.GainExperience(xp);
                }
            }
        }

        public static void Write(float xp, int target, int killer = -1) {
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncExperience);
                packet.Write(target);
                if (target != killer) {
                    packet.Write(xp * TerrariaMoba.nonKillXpRatio);
                }
                else {
                    packet.Write(xp);
                }
                packet.Send();
            }
            else if (Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte)Message.SyncExperience);
                packet.Write(xp);
                packet.Send(target);
            }
        }
    }
}