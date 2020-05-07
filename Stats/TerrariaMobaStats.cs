using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using TerrariaMoba.Characters;

namespace TerrariaMoba.Stats {
    public class TerrariaMobaStats {
        public Character MyCharacter;
        private int xpPerLevel = 100;
        public int experience = 0;
        
        public void GainExperience(int xp) {
            experience += xp;

            while (experience >= xpPerLevel) {
                MyCharacter.LevelUp();
                experience -= xpPerLevel;
            }
        }
    }
}