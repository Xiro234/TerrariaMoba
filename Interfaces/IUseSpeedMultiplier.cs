using Terraria;

namespace TerrariaMoba.Interfaces {
    public interface IUseSpeedMultiplier : IAbilityEffectInterface {
        float UseSpeedMultiplier(ref Item item);
    }
}