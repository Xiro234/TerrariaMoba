using Terraria;
using TerrariaMoba.Enums;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Players;
using TerrariaMoba.Stats;

namespace TerrariaMoba.Utils {
    public struct SylviaUtils {
        public const float VerdantFuryBuff = 1.25f;
        public const float VerdantFuryIncrease = 0.05f;
        public const int VerdantFuryBaseTime = 3;
        public static int JunglesWrathTime = 3;
        public const int AbilityOneBaseCooldown = 32;
        public const int AbilityTwoBaseCooldown = 36;
        
        
        public static float GetVerdantFuryIncrease() {
            return VerdantFuryBuff + (VerdantFuryIncrease * (Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().stats.MyCharacter.level - 1));
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

        public static int GetJunglesWrathTime() {
            return JunglesWrathTime * 60;
        }
    }
}