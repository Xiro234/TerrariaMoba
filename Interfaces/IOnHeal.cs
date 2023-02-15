namespace TerrariaMoba.Interfaces; 

public interface IOnHeal : IAbilityEffectInterface {
    void OnHeal(ref int amount, ref bool doText);
}