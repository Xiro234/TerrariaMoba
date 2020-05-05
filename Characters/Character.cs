using Steamworks;
using Terraria;

namespace TerrariaMoba.Characters {
    public abstract class Character {
        public int level = 1;
        public bool canSelectTalent = false;
        public bool[,] talentArray = new bool[7, 4];
        
        public abstract void AbilityOne();
        public abstract void AbilityOneAnimation(ref int animCounter);
        public abstract void AbilityTwo();
        public abstract void TalentSelect();
        public abstract void LevelUp();
        
        public void LevelTalentOne() {
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
        
        public void LevelTalentTwo() {
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
        
        public void LevelTalentThree() {
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