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
        public int level = 1;

        public void GainExperience(int xp, int team) {
            experience += xp;

            if (experience / level >= xpPerLevel) { //Checks if it needs to level up
                level += 1;
            }
        }
    }
}