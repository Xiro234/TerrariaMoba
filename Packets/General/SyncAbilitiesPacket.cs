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
                int length = reader.ReadInt32();
                Ability ability = (Ability)TerrariaMobaUtils.DeserializeFromBytes(reader.ReadBytes(length));
                Write(ability);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                int length = reader.ReadInt32();
                Ability ability = (Ability)TerrariaMobaUtils.DeserializeFromBytes(reader.ReadBytes(length));
                int whoCast = reader.ReadInt32();
                ability.OnCast();
            }
        }

        public static void Write(Ability ability) {
            if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncAbilities);
                byte[] bytes = TerrariaMobaUtils.SerializeToBytes(ability);
                packet.Write(bytes.Length);
                packet.Write(bytes);
                packet.Send();
            }
        }
    }
}