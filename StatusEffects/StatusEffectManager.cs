using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Network;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects {
    public class StatusEffectManager {
        private static Dictionary<Type, int> StatusEffectDict { get; set; }
        private static List<Type> StatusEffectTypesList { get; set; }
        
        #region NETWORK
        public static bool AddEffect(Player player, StatusEffect statusEffect, bool quiet = false) {
            statusEffect.SetPlayer(player);
            statusEffect.Apply();
            player.GetModPlayer<MobaPlayer>().EffectList.Add(statusEffect);

            if (!quiet) {
                NetworkHandler.SendAddEffect(statusEffect, player.whoAmI);
            }
            //TODO - Add error checking for adding effect
            return true;
        }

        public static bool RemoveEffect(Player player, StatusEffect statusEffect, bool quiet = false) {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            bool returnVar = mobaPlayer.EffectList.Remove(statusEffect);

            if (returnVar && !quiet) {
                NetworkHandler.SendSyncEffectList(player.whoAmI, player.whoAmI);
            }
            
            return returnVar;
        }
        
        public static bool HasEffect(Player player, StatusEffect effect) {
            int id = GetIDOfEffect(effect);
            return player.GetModPlayer<MobaPlayer>().EffectList.Any(x => GetIDOfEffect(x) == id);
        }

        public static bool SyncSingleEffect(Player player, StatusEffect statusEffect) {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            int index = mobaPlayer.EffectList.IndexOf(statusEffect);

            if (index >= 0) {
                NetworkHandler.SendSyncEffect(index, player.whoAmI, Main.myPlayer);
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        
        public static StatusEffect GetNewEffectInstance(int ID) {
            TerrariaMoba.Instance.Logger.Info(ID);

            return (StatusEffect)Activator.CreateInstance(StatusEffectTypesList[ID]);
        }

        public static int GetIDOfEffect(StatusEffect effect) {
            return StatusEffectDict[effect.GetType()];
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