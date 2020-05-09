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
        public int VerdantFuryTime = 3;
        public int JunglesWrathTime = 3;

        public float GetVerdantFuryIncrease() {
            return VerdantFuryBuff + (VerdantFuryIncrease * (Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().MyCharacter.level - 1));
        }

        public int GetVerdantFuryTime() {
            return VerdantFuryTime * 60;
        }

        public int GetJunglesWrathTime() {
            return JunglesWrathTime * 60;
        }
    }
}