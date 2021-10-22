using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Players;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Network {
    public static class NetworkHandler {
        public enum NetTag : byte {
            PVP_HIT = 0,
            ADD_STATUS_EFFECT,
            SYNC_STATUS_EFFECT,
            SYNC_EFFECT_LIST,
            START_GAME,
            ASSIGN_CHARACTER
        }
        
        public static void HandlePacket(BinaryReader reader, int sender) {
            NetTag tag = (NetTag)reader.ReadByte();
            switch (tag) {
                case NetTag.PVP_HIT:
                    ReceivePvpHit(reader, sender);
                    break;
                case NetTag.ADD_STATUS_EFFECT:
                    ReceiveAddEffect(reader, sender);
                    break;
                case NetTag.SYNC_STATUS_EFFECT:
                    ReceiveSyncEffect(reader, sender);
                    break;
                case NetTag.SYNC_EFFECT_LIST:
                    ReceiveSyncEffectList(reader, sender);
                    break;
                case NetTag.START_GAME:
                    ReceiveStartGame(reader, sender);
                    break;
                case NetTag.ASSIGN_CHARACTER:
                    RecieveAssignCharacter(reader, sender);
                    break;
                default:
                    //TODO - Add error logging
                    break;
            }
        }

        #region PVP_HIT
        public static void SendPvpHit(int physicalDamage, int magicalDamage, int trueDamage, int target, int killer) {
            ModPacket modPacket = TerrariaMoba.Instance.GetPacket();
            modPacket.Write((byte)NetTag.PVP_HIT);
            modPacket.Write(physicalDamage);
            modPacket.Write(magicalDamage);
            modPacket.Write(trueDamage);
            modPacket.Write((byte)target);
            modPacket.Write((byte)killer);
            modPacket.Send(ignoreClient: killer);
        }

        public static void ReceivePvpHit(BinaryReader reader, int sender) {
            int physicalDamage = reader.ReadInt32();
            int magicalDamage = reader.ReadInt32();
            int trueDamage = reader.ReadInt32();
            int target = reader.ReadByte();
            int killer = reader.ReadByte();
            
            if (killer != Main.myPlayer) {
                Main.player[target].GetModPlayer<MobaPlayer>().TakePvpDamage(physicalDamage, magicalDamage, trueDamage, killer, true);
            }

            if (Main.netMode == NetmodeID.Server) {
                SendPvpHit(physicalDamage, magicalDamage, trueDamage, target, killer);
            }
        }
        #endregion
        
        #region ADD_EFFECT
        public static void SendAddEffect(StatusEffect effect, int target, int ignore = -1) {
            int ID = StatusEffectManager.GetIDOfEffect(effect);

            ModPacket modPacket = TerrariaMoba.Instance.GetPacket();
            modPacket.Write((byte)NetTag.ADD_STATUS_EFFECT);
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
            TerrariaMoba.Instance.Logger.Error(index + " " + Main.player[target].GetModPlayer<MobaPlayer>().EffectList.Count);
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
        
        #region SYNC_EFFECT_LIST
        public static void SendSyncEffectList(int target, int ignore = -1) {
            var mobaPlayer = Main.player[target].GetModPlayer<MobaPlayer>();
            ModPacket modPacket = TerrariaMoba.Instance.GetPacket();
            modPacket.Write((byte)NetTag.SYNC_EFFECT_LIST);
            modPacket.Write(target);
            int count = mobaPlayer.EffectList.Count;
            modPacket.Write(count);
            StatusEffectManager.WriteListToPacket(modPacket, mobaPlayer.EffectList);
            modPacket.Send(ignoreClient: ignore);
        }

        public static void ReceiveSyncEffectList(BinaryReader reader, int sender) {
            int whoAmI = reader.ReadInt32();
            var target = Main.player[whoAmI].GetModPlayer<MobaPlayer>();
            int count = reader.ReadInt32();
            StatusEffectManager.ReadListFromPacket(reader, target, count);
            
            if (Main.netMode == NetmodeID.Server) {
                SendSyncEffectList(whoAmI, sender);
            }
        }
        #endregion

        #region START_GAME
        public static void SendStartGame(int ignore = -1) {
            ModPacket modPacket = TerrariaMoba.Instance.GetPacket();
            modPacket.Write((byte)NetTag.START_GAME);
            modPacket.Send(ignoreClient: ignore);
        }

        public static void ReceiveStartGame(BinaryReader reader, int sender) {
            TerrariaMobaUtils.StartGame();
            if (Main.netMode == NetmodeID.Server) {
                MobaSystem.MatchInProgress = true;
                NetMessage.SendData(MessageID.WorldData);
                SendStartGame(sender);
            }
        }
        #endregion

        #region ASSIGN_CHARACTER
        public static void SendAssignCharacter(int target, int ignore = -1) {
            var mobaPlayer = Main.player[target].GetModPlayer<MobaPlayer>();
            ModPacket modPacket = TerrariaMoba.Instance.GetPacket();
            modPacket.Write((byte) NetTag.ASSIGN_CHARACTER);
            modPacket.Write(target);
            int ID = CharacterManager.CharacterDict[mobaPlayer.selectedCharacter];
            modPacket.Write(ID);
            modPacket.Send(ignoreClient: ignore);
        }

        public static void RecieveAssignCharacter(BinaryReader reader, int sender) {
            int whoAmI = reader.ReadInt32();
            var target = Main.player[whoAmI].GetModPlayer<MobaPlayer>();
            int ID = reader.ReadInt32();
            target.selectedCharacter = CharacterManager.CharacterTypesList[ID];
            //Main.NewText("woo");
            if (Main.netMode == NetmodeID.Server) {
                SendAssignCharacter(target.Player.whoAmI, sender);
            }
        }
        #endregion
    }
}