namespace TerrariaMoba.Interfaces {
    public interface ITakePvpDamage : IAbilityEffectInterface {
        void TakePvpDamage(ref int damage, ref int killer);
    }
}