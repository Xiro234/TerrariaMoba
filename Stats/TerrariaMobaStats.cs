using System;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using TerrariaMoba.Characters;
using TerrariaMoba;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace TerrariaMoba.Stats {
    public class TerrariaMobaStats {
        public Character MyCharacter = new Sylvia();
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