using Terraria.DataStructures;

namespace TerrariaMoba.Interfaces {
    public interface IDrawEffects : IAbilityEffectInterface {
        void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright);
    }
}