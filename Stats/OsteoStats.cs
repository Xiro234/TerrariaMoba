namespace TerrariaMoba.Stats {
    public class OsteoStats {
        public Stat MaxHealth;
        public Stat MaxResource;
        public Stat A1SkeleDmg;
        public Stat A1SkeleHealth;
        public Stat A2Dmg;
        public Stat U1Dmg;

        public OsteoStats() {
            MaxHealth = new Stat(2060, 1.04f);
            MaxResource = new Stat(500, 1.04f);
            A1SkeleDmg = new Stat(160, 1.04f);
            A1SkeleHealth = new Stat(400, 1.04f);
            A2Dmg = new Stat(250, 1.04f);
            U1Dmg = new Stat(500, 1.05f);
        }
        
        public void LevelUp() {
            MaxHealth.ScaleValue();
            MaxResource.ScaleValue();
            A1SkeleDmg.ScaleValue();
            A1SkeleHealth.ScaleValue();
            A2Dmg.ScaleValue();
            U1Dmg.ScaleValue();
        }
    }
}