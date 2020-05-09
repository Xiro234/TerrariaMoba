using System.IO;
using System.Xml.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using TerrariaMoba.Enums;
using TerrariaMoba.Buffs;
using TerrariaMoba.Players;
using TerrariaMoba.Stats;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Packets {
    public class SyncPvpHitPacket {
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                int damage = reader.ReadInt32();
                int killer = reader.ReadInt32();
                bool sendThorns = reader.ReadBoolean();
                Write(target, damage, killer, sendThorns);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                int damage = reader.ReadInt32();
                int killer = reader.ReadInt32();
                bool sendThorns = reader.ReadBoolean();
                Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().DamageOverride(damage, Main.LocalPlayer, killer, sendThorns);
            }
        }
    
        public static void Write(int target, int damage, int killer, bool sendThorns) {
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncPvpHit);
                packet.Write(target);
                packet.Write(damage);
                packet.Write(killer);
                packet.Write(sendThorns);
                packet.Send();
            }
            else if (Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte)Message.SyncPvpHit);
                packet.Write(damage);
                packet.Write(killer);
                packet.Write(sendThorns);
                packet.Send(target);
            }
        }
    }
}