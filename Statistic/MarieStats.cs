namespace TerrariaMoba.Statistic {
    public class MarieStats {
        public Stat MaxHealth;
        public Stat MaxResource;
        public Stat A1BottleDmg;
        public Stat A1WhirlpoolDmg;
        public Stat A2Heal;
        public Stat U2RainDmg;
        public Stat U2LightningDmg;

        public MarieStats() {
            MaxHealth = new Stat(1460, 1.04f);
            MaxResource = new Stat(500, 1.04f);
            A1BottleDmg = new Stat(120, 1.04f);
            A1WhirlpoolDmg = new Stat(60, 1.04f);
            A2Heal = new Stat(200, 1.04f);
            U2RainDmg = new Stat(54, 1.035f);
            U2LightningDmg = new Stat(142, 1.045f);
        }

        public void LevelUp() {
            MaxHealth.ScaleValue();
            MaxResource.ScaleValue();
            A1BottleDmg.ScaleValue();
            A1WhirlpoolDmg.ScaleValue();
            A2Heal.ScaleValue();
            U2RainDmg.ScaleValue();
            U2LightningDmg.ScaleValue();
        }
    }
}