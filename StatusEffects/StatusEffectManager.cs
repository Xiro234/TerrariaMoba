using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Network;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects {
    public class StatusEffectManager {
        private static Dictionary<Type, int> StatusEffectDict { get; set; }
        private static List<Type> StatusEffectTypesList { get; set; }
        
        #region NETWORK
        public static bool AddEffect(Player Player, StatusEffect statusEffect, bool quiet = false) {
            if (HasEffect(Player, statusEffect)) {
                var mobaPlayer = Player.GetModPlayer<MobaPlayer>();

                int index = mobaPlayer.EffectList.FindIndex(match => match.ID == statusEffect.ID);

                if (index != -1) {
                    mobaPlayer.EffectList[index].ReApply();
                    Logging.PublicLogger.Debug("StatusEffectManager: Found existing effect, reapplied: " + statusEffect.DisplayName);
                }
            }
            else {
                statusEffect.SetPlayer(Player);
                statusEffect.Apply();
                statusEffect.ConstructFlatAttributes();
                statusEffect.ConstructMultAttributes();
                Player.GetModPlayer<MobaPlayer>().EffectList.Add(statusEffect);
                Logging.PublicLogger.Debug("StatusEffectManager: " + statusEffect.DisplayName + " added.");
            }

            if (!quiet && Main.netMode != NetmodeID.SinglePlayer) {
                NetworkHandler.SendAddEffect(statusEffect, Player.whoAmI);
            }
            //TODO - Add error checking for adding effect
            return true;
        }

        public static bool RemoveEffect(Player Player, StatusEffect statusEffect, bool quiet = false) {
            var mobaPlayer = Player.GetModPlayer<MobaPlayer>();
            int index = mobaPlayer.EffectList.IndexOf(statusEffect);
            
            if (index >= 0) {
                mobaPlayer.EffectList.RemoveAt(index);

                if (!quiet && Main.netMode != NetmodeID.SinglePlayer) {
                    NetworkHandler.SendSyncRemoveEffect(index, Player.whoAmI);
                }
            }
            
            return index >= 0;
        }
        
        public static bool RemoveEffect(Player Player, int index, bool quiet = false) {
            var mobaPlayer = Player.GetModPlayer<MobaPlayer>();

            if (index >= 0) {
                mobaPlayer.EffectList.RemoveAt(index);

                if (!quiet && Main.netMode != NetmodeID.SinglePlayer) {
                    NetworkHandler.SendSyncRemoveEffect(index, Player.whoAmI);
                }
            }
            
            return index >= 0;
        }
        
        public static bool HasEffect(Player Player, StatusEffect effect) {
            int id = GetIDOfEffect(effect);
            return Player.GetModPlayer<MobaPlayer>().EffectList.Any(x => GetIDOfEffect(x) == id);
        }

        public static bool SyncSingleEffect(Player Player, StatusEffect statusEffect) {
            var mobaPlayer = Player.GetModPlayer<MobaPlayer>();
            int index = mobaPlayer.EffectList.IndexOf(statusEffect);

            if (index >= 0) {
                NetworkHandler.SendSyncEffect(index, Player.whoAmI, Main.myPlayer);
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        
        public static StatusEffect GetNewEffectInstance(int ID) {
            StatusEffect effect = (StatusEffect)Activator.CreateInstance(StatusEffectTypesList[ID]);
            if (effect == null) {
                TerrariaMoba.Instance.Logger.Debug("Null!");
            }
            else {
                //TerrariaMoba.Instance.Logger.Debug(effect.DisplayName);
            }

            return effect;
        }
        
        public static int GetIDOfEffect(StatusEffect effect) {
            return StatusEffectDict[effect.GetType()];
        }

        public static bool PlayerHasEffectType<T>(Player player) {
            MobaPlayer mobaPlayer = player.GetModPlayer<MobaPlayer>();
            return mobaPlayer.EffectList.OfType<T>().Any();
        }

        public static void Load() {
            StatusEffectDict = new Dictionary<Type, int>();
            StatusEffectTypesList = new List<Type>();
            PopulateEffectDictionary();
        }
        
        public static void PopulateEffectDictionary() {
            var types = typeof(StatusEffect).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(StatusEffect)) && !type.IsAbstract);
            
            List<Type> typesList = types.ToList();

            for (int i = 0; i < typesList.Count(); i++) {
                StatusEffectDict.Add(typesList[i], i);
            }

            StatusEffectTypesList = typesList;
        }

        public static void WriteListToPacket(ModPacket modPacket, List<StatusEffect> effectList) {
            foreach (var effect in effectList) {
                modPacket.Write(GetIDOfEffect(effect));
                effect.SendEffectElements(modPacket);
            }
        }

        public static void ReadListFromPacket(BinaryReader reader, MobaPlayer target, int count) {
            target.EffectList = new List<StatusEffect>();

            for (int i = 0; i < count; i++) {
                int id = reader.ReadInt32();
                var effect = GetNewEffectInstance(id);
                effect.ReceiveEffectElements(reader);
                target.EffectList.Add(effect);
            }
        }
    }
}