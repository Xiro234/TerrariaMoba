using Terraria;
using TerrariaMoba;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Players;
using TerrariaMoba.Stats;

namespace TerrariaMoba.Stats {
    public class SylviaStats {
        public float VerdantFuryBuff = 1.25f;
        public float VerdantFuryIncrease = 0.05f;
        public int VerdantFuryBaseTime = 3;
        public int JunglesWrathBaseTime = 3;
        public int AbilityOneBaseCooldown = 32; //32
        public int AbilityTwoBaseCooldown = 36; //36

        public float GetVerdantFuryIncrease() {
            return VerdantFuryBuff + (VerdantFuryIncrease * (Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().MyCharacter.level - 1));
        }

        public int GetAbilityOneCd() {
            return AbilityOneBaseCooldown * 60;
        }
        
        public int GetAbilityTwoCd() {
            return AbilityTwoBaseCooldown * 60;
        }
        
        public int GetVerdantFuryTime() {
            return VerdantFuryBaseTime * 60;
        }

        public int GetJunglesWrathTime() {
            return JunglesWrathBaseTime * 60;
        }
    }
}