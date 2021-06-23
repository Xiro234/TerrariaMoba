namespace TerrariaMoba.Interfaces {
    public interface ITakePvpDamage : IAbilityEffectInterface {
        void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer);
    }
}