using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TerrariaMoba.Network;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects {
    public class StatusEffectManager {
        private static Dictionary<Type, int> StatusEffectDict { get; set; }
        private static List<Type> StatusEffectTypesList { get; set; }

        public static bool AddEffect(Player player, StatusEffect statusEffect, bool quiet = false) {
            statusEffect.SetPlayer(player);
            statusEffect.Apply();
            player.GetModPlayer<MobaPlayer>().EffectList.Add(statusEffect);

            if (!quiet) {
                NetworkHandler.SendAddEffect(statusEffect, player.whoAmI);
            }
            return true;
        }

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
    }
}