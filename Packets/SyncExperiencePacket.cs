using System.IO;
using System.Xml.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Packets {
    public class SyncExperiencePacket {

        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                float xp = reader.ReadSingle();
                Write(xp, target);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                var plr = Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>();
                int xp = (int)reader.ReadSingle();
                plr.stats.GainExperience(xp);
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