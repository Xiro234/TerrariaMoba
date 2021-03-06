using System.IO;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Effects;
using TerrariaMoba.Players;
using WebmilioCommons.Networking;
using WebmilioCommons.Networking.Packets;

namespace TerrariaMoba.Packets.General {
    public class EffectPacket : ModPlayerNetworkPacket<MobaPlayer>{
        public override NetworkPacketBehavior Behavior { get => NetworkPacketBehavior.SendToAllClients; }

        public int index = -1;
        
        public Effect effect {
            get => base.ModPlayer.effectList[index];
            set { }
        }

        public EffectPacket(Effect newEffect) {
            index = EffectManager.GetIndexOfEffect(effect);
        }

        protected override bool PreSend(ModPacket modPacket, int? fromWho = null, int? toWho = null) {
            if (index < 0) {
                return false;
            }
            return base.PreSend(modPacket, fromWho , toWho);
        }
    }
}