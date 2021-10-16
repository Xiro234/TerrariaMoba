/*using TerrariaMoba.Players;
using WebmilioCommons.Networking.Packets;


namespace TerrariaMoba.Packets.General {
    public class TalentsPacket : ModPlayerNetworkPacket<MobaPlayer> {

        /*protected override void Receive() {
            if (Main.netMode != NetmodeID.Server) {
                var plr = Main.player[target].GetModPlayer<MobaPlayer>();
                if (plr.CharacterPicked != true) {
                    AssignCharacter(ref plr.MyCharacter, character, plr.Player);
                    plr.CharacterPicked = true;
                }
            }
            else {
                new TalentsPacket() {
                    target = target,
                    character = character
                }.Send(Node.FromServer(), null, false);
            }
        }#1#
    }
}*/