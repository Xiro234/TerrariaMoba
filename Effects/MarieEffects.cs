namespace TerrariaMoba.Effects {
    public class MarieEffects {
        public bool Floodboost;
        public bool LacusianBlessing;

        public MarieEffects() {
            Floodboost = false;
            LacusianBlessing = false;
        }

        public void ResetEffects() {
            Floodboost = false;
            LacusianBlessing = false;
        }
    }
}