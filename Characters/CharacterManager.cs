using System;
using System.Collections.Generic;
using System.Linq;

namespace TerrariaMoba.Characters {
    public class CharacterManager {
        public static Dictionary<Type, int> CharacterDict { get; private set; }
        public static List<Type> CharacterTypesList { get; private set; }

        public static int GetIDofCharacter(Character character) {
            return CharacterDict[character.GetType()];
        }

        public static Character GetNewCharacterInstance(int ID) {
            return (Character) Activator.CreateInstance(CharacterTypesList[ID]);
        }

        public static void Load() {
            CharacterDict = new Dictionary<Type, int>();
            CharacterTypesList = new List<Type>();
            PopulateCharacterDictionary();
        }

        public static void PopulateCharacterDictionary() {
            var types = typeof(Character).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Character)) && !type.IsAbstract);
            
            List<Type> typesList = types.ToList();

            for (int i = 0; i < typesList.Count(); i++) {
                CharacterDict.Add(typesList[i], i);
            }

            CharacterTypesList = typesList;
        }
    }
}