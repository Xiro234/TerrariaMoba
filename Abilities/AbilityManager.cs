using System;
using System.Collections.Generic;
using System.Linq;

namespace TerrariaMoba.Abilities {
    public static class AbilityManager {
        public static Dictionary<Type, int> AbilityDict { get; private set; }
        public static List<Type> AbilityTypesList { get; private set; }

        public static int GetIDofAbility(Ability Ability) {
            return AbilityDict[Ability.GetType()];
        }

        public static Ability GetNewAbilityInstance(int ID) {
            return (Ability) Activator.CreateInstance(AbilityTypesList[ID]);
        }

        public static void Load() {
            AbilityDict = new Dictionary<Type, int>();
            AbilityTypesList = new List<Type>();
            PopulateAbilityDictionary();
        }

        public static void PopulateAbilityDictionary() {
            var types = typeof(Ability).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Ability)) && !type.IsAbstract);
            
            List<Type> typesList = types.ToList();

            for (int i = 0; i < typesList.Count(); i++) {
                AbilityDict.Add(typesList[i], i);
            }

            AbilityTypesList = typesList;
        }
    }
}