using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Network {
    public static class NetworkHandler {
        public enum NetTag : byte {
            PVP_HIT = 0,
            STATUS_EFFECT,
            SYNC_STATUS_EFFECT,
            
        }
        
        public static void HandlePacket(BinaryReader reader, int sender) {
            NetTag tag = (NetTag)reader.ReadByte();
            switch (tag) {
                case NetTag.PVP_HIT:
                    ReceivePvpHit(reader, sender);
                    break;
                case NetTag.STATUS_EFFECT:
                    ReceiveAddEffect(reader, sender);
                    break;
                default:
                    //TODO - Add error logging
                    break;
            }
        }

        #region PVP_HIT
        public static void SendPvpHit(int damage, int target, int killer) {
            ModPacket modPacket = TerrariaMoba.Instance.GetPacket();
            modPacket.Write((byte)NetTag.PVP_HIT);
            modPacket.Write(damage);
            modPacket.Write((byte)target);
            modPacket.Write((byte)killer);
            modPacket.Send();
        }

        public static void ReceivePvpHit(BinaryReader reader, int sender) {
            int damage = reader.ReadInt32();
            int target = reader.ReadByte();
            int killer = reader.ReadByte();
            
            if (killer != Main.myPlayer) {
                Main.player[target].GetModPlayer<MobaPlayer>().TakePvpDamage(damage, killer, true);
            }

            if (Main.netMode == NetmodeID.Server) {
                SendPvpHit(damage, target, killer);
            }
        }
        #endregion
        
        #region ADD_EFFECT
        public static void SendAddEffect(StatusEffect effect, int target, int ignore = -1) {
            int ID = StatusEffectManager.GetIDOfEffect(effect);

            ModPacket modPacket = TerrariaMoba.Instance.GetPacket();
            modPacket.Write((byte)NetTag.STATUS_EFFECT);
            modPacket.Write(ID);
            modPacket.Write(target);
            effect.SendEffectElements(modPacket);
            modPacket.Send(ignoreClient: ignore);
        }

        public static void ReceiveAddEffect(BinaryReader reader, int sender) {
            int ID = reader.ReadInt32();
            int target = reader.ReadInt32();
            StatusEffect effect = StatusEffectManager.GetNewEffectInstance(ID);

            effect.ReceiveEffectElements(reader);

            StatusEffectManager.AddEffect(Main.player[target], effect, true);
            
            if (Main.netMode == NetmodeID.Server) {
                SendAddEffect(effect, target, sender);
            }
        }
        #endregion
        
        #region SYNC_EFFECT
        public static void SendSyncEffect(int index, int target, int ignore = -1) {
            ModPacket modPacket = TerrariaMoba.Instance.GetPacket();
            modPacket.Write((byte)NetTag.SYNC_STATUS_EFFECT);
            modPacket.Write(index);
            modPacket.Write(target);
            Main.player[target].GetModPlayer<MobaPlayer>().EffectList[index].SendEffectElements(modPacket);
            modPacket.Send(ignoreClient: ignore);
        }

        public static void ReceiveSyncEffect(BinaryReader reader, int sender) {
            int index = reader.ReadInt32();
            int target = reader.ReadInt32();
            Main.player[target].GetModPlayer<MobaPlayer>().EffectList[index].ReceiveEffectElements(reader);
            
            if (Main.netMode == NetmodeID.Server) {
                SendSyncEffect(index, target, sender);
            }
        }
        #endregion
    }
}