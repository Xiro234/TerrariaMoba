using System;
using Steamworks;
using Terraria;

namespace TerrariaMoba.Characters {
    public abstract class Character {
        public int level = 1;
        public bool canSelectTalent = false;
        public bool[,] talentArray = new bool[7, 4];
        private int xpPerLevel = 100;
        public int experience = 0;
        
        public virtual void GainExperience(int xp) {
            experience += xp;

            while (experience >= xpPerLevel) {
                LevelUp();
                experience -= xpPerLevel;
            }
        }

        public String AbilityOneName;
        public int AbilityOneCooldown;
        public int AbilityOneCooldownTimer = 0;

        public String AbilityTwoName = "";
        public int AbilityTwoCooldown = 0;
        public int AbilityTwoCooldownTimer = 0;

        public String UltimateName = "";
        public int UltimateCooldown = 0;
        public int UltimateCooldownTimer = 0;

        public abstract void AbilityOne();
        public abstract void AbilityTwo();

        public abstract void Ultimate();
        public abstract void TalentSelect();
        public abstract void LevelUp();
        
        public virtual void LevelTalentOne() {
            if (canSelectTalent) {
                switch (level) {
                    case 2:
                        talentArray[0, 0] = true;
                        break;
                    case 4:
                        break;
                    case 7:
                        break;
                    case 10:
                        break;
                    case 13:
                        break;
                    case 16:
                        break;
                    case 20:
                        break;
                }
                canSelectTalent = false;
            }
        }
        
        public virtual void LevelTalentTwo() {
            if (canSelectTalent) {
                switch (level) {
                    case 2:
                        talentArray[0, 1] = true;
                        break;
                    case 4:
                        break;
                    case 7:
                        break;
                    case 10:
                        break;
                    case 13:
                        break;
                    case 16:
                        break;
                    case 20:
                        break;
                }
                canSelectTalent = false;
            }
        }
        
        public virtual void LevelTalentThree() {
            if (canSelectTalent) {
                switch (level) {
                    case 2:
                        talentArray[0, 2] = true;
                        break;
                    case 4:
                        break;
                    case 7:
                        break;
                    case 10:
                        break;
                    case 13:
                        break;
                    case 16:
                        break;
                    case 20:
                        break;
                }
                canSelectTalent = false;
            }
        }
    }
}