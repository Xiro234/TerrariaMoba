using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.GenericEffects {
    public abstract class DamageOverTime : StatusEffect {
        private int physicalDamage;
        private int magicalDamage;
        private int trueDamage;
        private int timer;

        public DamageOverTime() { }
        public DamageOverTime(int phys, int mag, int tru, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            physicalDamage = phys;
            magicalDamage = mag;
            trueDamage = tru;
            timer = 0;
        }

        public override void WhileActive() {
            if(timer == 0) {
                timer = 60;
                User.GetModPlayer<MobaPlayer>().TakePvpDamage(physicalDamage, magicalDamage, trueDamage, ApplicantID, true);
            } else {
                timer--;
            }

            base.WhileActive();
        }
    }
}
