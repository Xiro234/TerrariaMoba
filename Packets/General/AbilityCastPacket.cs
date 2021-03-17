/*using System.IO;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;
using WebmilioCommons.Networking;
using WebmilioCommons.Networking.Packets;

namespace TerrariaMoba.Packets.General {
    public class AbilityCastPacket : ModPlayerNetworkPacket<MobaPlayer>{
        public override NetworkPacketBehavior Behavior { get => NetworkPacketBehavior.SendToAllClients; }

        public Ability ability {
            get => ModPlayer.MyCharacter.Abilities[index];
            set => ModPlayer.MyCharacter.Abilities[index] = value;
        }

        public int index = -1;

        protected override bool PostReceive(BinaryReader reader, int fromWho) {
            if (index >= 0) {
                ModPlayer.MyCharacter.HandleAbility(ability);
                return true;
            }
            return false;
        }

        protected override bool PreSend(ModPacket modPacket, int? fromWho = null, int? toWho = null) {
            if (index < 0) {
                return false;
            }
            return base.PreSend(modPacket, fromWho , toWho);
        }
    }
}*/