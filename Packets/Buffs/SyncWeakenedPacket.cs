using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Buffs;
using TerrariaMoba.Players;
using TerrariaMoba.Stats;
using static Terraria.ModLoader.ModContent;
    
namespace TerrariaMoba.Packets {
    public class SyncWeakenedPacket {
    
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int target = reader.ReadInt32();
                int duration = reader.ReadInt32();
                float percentAdd = reader.ReadSingle();
                String name = reader.ReadString();
                Write(target, duration, percentAdd, name);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                int duration = reader.ReadInt32();
                float percentAdd = reader.ReadSingle();
                String name = reader.ReadString();

                bool add = true;
                foreach (var item in Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().ReducedDamageList) {
                    if (item.Item1 == name) {
                        add = false;
                    }
                }

                if (add) {
                    Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().ReducedDamageList
                        .Add(Tuple.Create(name, percentAdd, duration));
                }

                Main.LocalPlayer.AddBuff(BuffType<Buffs.Weakened>(), duration, false);
            }
        }
    
        public static void Write(int target, int duration, float percentAdd, String name) {
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncWeakened);
                packet.Write(target);
                packet.Write(duration);
                packet.Write(percentAdd);
                packet.Write(name);
                packet.Send();
            }
            if (Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncWeakened);
                packet.Write(duration);
                packet.Write(percentAdd);
                packet.Write(name);
                packet.Send(target);
            }
        }
    }
}