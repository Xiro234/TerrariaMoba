namespace TerrariaMoba.Stats {
    public class SylviaStats {
        public Stat MaxHealth;
        public Stat MaxResource;
        public Stat A1VineDmg;
        public Stat U1JavelinDmg;
        public Stat U2HeadDmg;
        public Stat U2SporeDmg;

        public SylviaStats() {
            MaxHealth = new Stat(1340, 1.04f);
            MaxResource = new Stat(500, 1.04f);
            A1VineDmg = new Stat(230, 1.04f);
            U1JavelinDmg = new Stat(420, 1.04f);
            U2HeadDmg = new Stat(540, 1.05f);
            U2SporeDmg = new Stat(120, 1.035f);
        }
        
        public void LevelUp() {
            MaxHealth.ScaleValue();
            MaxResource.ScaleValue();
            A1VineDmg.ScaleValue();
            U1JavelinDmg.ScaleValue();
            U2HeadDmg.ScaleValue();
            U2SporeDmg.ScaleValue();
        }
    }
}