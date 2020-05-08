using Terraria;
using TerrariaMoba;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Players;
using TerrariaMoba.Stats;

namespace TerrariaMoba.Stats {
    public static class SylviaStats {
        public static float VerdantFuryBuff = 1.25f;
        public static float VerdantFuryIncrease = 0.05f;
        public static int VerdantFuryBaseTime = 3;
        public static int JunglesWrathBaseTime = 3;
        public static int AbilityOneBaseCooldown = 32; //32
        public static int AbilityTwoBaseCooldown = 36; //36
        
        public static float GetVerdantFuryIncrease() {
            return VerdantFuryBuff + (VerdantFuryIncrease * (Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().MyCharacter.level - 1));
        }

        public static int GetAbilityOneBaseCd() {
            return AbilityOneBaseCooldown * 60;
        }
        
        public static int GetAbilityTwoBaseCd() {
            return AbilityTwoBaseCooldown * 60;
        }
        
        public static int GetVerdantFuryBaseTime() {
            return VerdantFuryBaseTime * 60;
        }

        public static int GetJunglesBaseWrathTime() {
            return JunglesWrathBaseTime * 60;
        }
    }
}