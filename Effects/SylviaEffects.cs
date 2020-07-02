namespace TerrariaMoba.Effects {
    public class SylviaEffects {
        public bool JunglesWrath;
        public int JunglesWrathCount;
        public float JunglesWrathDamage;
        public bool VerdantFury;
        public bool EnsnaringVines;

        public SylviaEffects() {
            JunglesWrath = false;
            JunglesWrathCount = 0;
            VerdantFury = false;
            EnsnaringVines = false;
        }

        public void ResetEffects() {
            JunglesWrath = false;
            VerdantFury = false;
            EnsnaringVines = false;
        }
    }
}