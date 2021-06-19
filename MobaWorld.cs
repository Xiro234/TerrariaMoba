using System.IO;
using Terraria.ModLoader;

namespace TerrariaMoba {
    public class MobaWorld : ModWorld {
        public static bool MatchInProgress = false;

        public override void NetSend(BinaryWriter writer) {
            writer.Write(MatchInProgress);
        }

        public override void NetReceive(BinaryReader reader) {
            MatchInProgress = reader.ReadBoolean();
        }

        public static void StartGame() {
            MatchInProgress = true;
        }
    }
}