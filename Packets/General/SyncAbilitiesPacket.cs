using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Packets {
    public class SyncAbilitiesPacket {
        public static void Read(BinaryReader reader) {
            if (Main.netMode == NetmodeID.Server) {
                int abilityNumber = reader.ReadInt32();
                int whoCast = reader.ReadInt32();
                Write(abilityNumber, whoCast);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) {
                int abilityNumber = reader.ReadInt32();
                int whoCast = reader.ReadInt32();
                if (Main.myPlayer != whoCast) {
                    var mobaPlayer = Main.player[whoCast].GetModPlayer<MobaPlayer>();
                    Main.NewText("Ability: " + abilityNumber + " Who: " + whoCast);
                    switch (abilityNumber) {
                        case (0):
                            mobaPlayer.MyCharacter.AbilityOneOnCast(Main.player[whoCast]);
                            break;
                        case (1):
                            mobaPlayer.MyCharacter.AbilityTwoOnCast(Main.player[whoCast]);
                            break;
                        case (2):
                            mobaPlayer.MyCharacter.UltimateOnCast(Main.player[whoCast]);
                            break;
                        case (3):
                            mobaPlayer.MyCharacter.TraitOnCast(Main.player[whoCast]);
                            break;
                        default:
                            Main.NewText("Invalid \"abilityNumber\" in SyncAbilitiesPacket");
                            break;
                    }
                }
            }
        }

        public static void Write(int abilityNumber, int whoCast) {
            if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server) {
                ModPacket packet = TerrariaMoba.Instance.GetPacket();
                packet.Write((byte) Message.SyncAbilities);
                packet.Write(abilityNumber);
                packet.Write(whoCast);
                packet.Send();
            }
        }
    }
}