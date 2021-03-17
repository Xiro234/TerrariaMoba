/*using Terraria.ModLoader;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.Players;
using WebmilioCommons.Networking;
using WebmilioCommons.Networking.Packets;

namespace TerrariaMoba.Packets.General {
    public class EffectPacket : ModPlayerNetworkPacket<MobaPlayer>{
        public override NetworkPacketBehavior Behavior { get => NetworkPacketBehavior.SendToAllClients; }

        public int index = -1;
        
        public StatusEffect StatusEffect {
            get => base.ModPlayer.EffectList[index];
            set { }
        }

        public EffectPacket(StatusEffect newStatusEffect) {
            index = StatusEffectManager.GetIndexOfEffect(StatusEffect);
        }

        protected override bool PreSend(ModPacket modPacket, int? fromWho = null, int? toWho = null) {
            if (index < 0) {
                return false;
            }
            return base.PreSend(modPacket, fromWho , toWho);
        }
    }
}*/