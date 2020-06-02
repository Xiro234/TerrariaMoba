using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.Packets;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Characters {
    public class Character {
        public int level = 1;
        public bool canSelectTalent = false;
        public string characterName = "none";
        public bool[,] talentArray = new bool[7, 4];
        public int xpPerLevel = 100;
        public int experience = 0;
        public CharacterEnum CharacterEnum;
        public int ultNumber; //WILL CHANGE TO TALENT SELECT

        public Character(Player player) {
            var plr = player.GetModPlayer<MobaPlayer>();
            plr.CharacterPicked = true;
        }
        
        public virtual void GainExperience(int xp) {
            experience += xp;

            while (experience >= xpPerLevel) {
                LevelUp();
                experience -= xpPerLevel;
            }
        }

        public Texture2D CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Lock");

        public Texture2D AbilityOneIcon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        public String AbilityOneName = "";
        public int AbilityOneCooldown = 0;
        public int AbilityOneCooldownTimer = 0;

        public Texture2D AbilityTwoIcon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        public String AbilityTwoName = "";
        public int AbilityTwoCooldown = 0;
        public int AbilityTwoCooldownTimer = 0;

        public Texture2D UltimateIcon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        public String UltimateName = "";
        public int UltimateCooldown = 0;
        public int UltimateCooldownTimer = 0;

        public Texture2D TraitIcon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        public String TraitName = "";
        public int TraitCooldown = 0;
        public int TraitCooldownTimer = 0;

        public virtual void AbilityOneOnCast(Player player) { }
        public virtual void AbilityOneInUse(Player player) { }
        public virtual void AbilityOneOnEnd(Player player) { }
        
        public virtual void AbilityTwoOnCast(Player player) { }
        public virtual void AbilityTwoInUse(Player player) { }
        public virtual void AbilityTwoOnEnd(Player player) { }

        public virtual void UltimateOnCast(Player player) { }
        public virtual void UltimateInUse(Player player) { }
        public virtual void UltimateOnEnd(Player player) { }
        
        public virtual void TraitOnCast(Player player) { }
        public virtual void TraitInUse(Player player) { }
        public virtual void TraitOnEnd(Player player) { }
        
        public virtual void TalentSelect() { }
        public virtual void LevelUp() { }
        
        public virtual void ChooseCharacter() { }

        public virtual void LevelTalentOne() {
            Main.NewText(canSelectTalent);
            Main.NewText(level);
            if (canSelectTalent) {
                switch (level) {
                    case 1:
                        Main.NewText("Level up one!");
                        talentArray[0, 0] = true;
                        break;
                    case 4:
                        talentArray[1, 0] = true;
                        break;
                    case 7:
                        talentArray[2, 0] = true;
                        break;
                    case 10:
                        talentArray[3, 0] = true;
                        break;
                    case 13:
                        talentArray[4, 0] = true;
                        break;
                    case 16:
                        talentArray[5, 0] = true;
                        break;
                    case 20:
                        talentArray[6, 0] = true;
                        break;
                }
                canSelectTalent = false;
                SyncCharacter();
            }
        }
        
        public virtual void LevelTalentTwo() {
            if (canSelectTalent) {
                switch (level) {
                    case 1:
                        Main.NewText("Level up two!");
                        talentArray[0, 1] = true;
                        break;
                    case 4:
                        talentArray[1, 1] = true;
                        break;
                    case 7:
                        talentArray[2, 1] = true;
                        break;
                    case 10:
                        talentArray[3, 1] = true;
                        break;
                    case 13:
                        talentArray[4, 1] = true;
                        break;
                    case 16:
                        talentArray[5, 1] = true;
                        break;
                    case 20:
                        talentArray[6, 1] = true;
                        break;
                }
                canSelectTalent = false;
                SyncCharacter();
            }
        }
        
        public virtual void LevelTalentThree() {
            if (canSelectTalent) {
                switch (level) {
                    case 1:
                        Main.NewText("Level up three!");
                        talentArray[0, 2] = true;
                        break;
                    case 4:
                        talentArray[1, 2] = true;
                        break;
                    case 7:
                        talentArray[2, 2] = true;
                        break;
                    case 10:
                        talentArray[3, 2] = true;
                        break;
                    case 13:
                        talentArray[4, 2] = true;
                        break;
                    case 16:
                        talentArray[5, 2] = true;
                        break;
                    case 20:
                        talentArray[6, 2] = true;
                        break;
                }
                canSelectTalent = false;
                SyncCharacter();
            }
        }

        public virtual void LevelTalentFour() {
            if (canSelectTalent) {
                switch (level) {
                    case 1:
                        talentArray[0, 3] = true;
                        break;
                    case 4:
                        talentArray[1, 3] = true;
                        break;
                    case 7:
                        talentArray[2, 3] = true;
                        break;
                    case 10:
                        talentArray[3, 3] = true;
                        break;
                    case 13:
                        talentArray[4, 3] = true;
                        break;
                    case 16:
                        talentArray[5, 3] = true;
                        break;
                    case 20:
                        talentArray[6, 3] = true;
                        break;
                }
                canSelectTalent = false;
                SyncCharacter();
            }
        }

        public virtual void SyncCharacter() {
            SyncCharacterPacket.Write(Main.LocalPlayer.whoAmI, CharacterEnum, talentArray);
        }
    }
}