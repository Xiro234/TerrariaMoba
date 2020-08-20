namespace TerrariaMoba.Stats {
    public class FlibnobStats {
        public Stat MaxHealth;
        public Stat MaxResource;
        public Stat A1FireballDmg;
        public Stat U2EarthDmg;

        public FlibnobStats() {
            MaxHealth = new Stat(2060, 1.04f);
            MaxResource = new Stat(500, 1.04f);
            A1FireballDmg = new Stat(265, 1.04f);
            U2EarthDmg = new Stat(480, 1.04f);
        }
        
        public void LevelUp() {
            MaxHealth.ScaleValue();
            MaxResource.ScaleValue();
            A1FireballDmg.ScaleValue();
            U2EarthDmg.ScaleValue();
        }
    }
}