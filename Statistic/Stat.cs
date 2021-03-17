namespace TerrariaMoba.Statistic {
    public class Stat {
        public float Value { get; set; }
        public float Scalar { get; }

        public Stat(float v, float s) {
            Value = v;
            Scalar = s;
        }

        public int ScaleValue() {
            Value *= Scalar;
            return (int) Value;
        }
    }
}