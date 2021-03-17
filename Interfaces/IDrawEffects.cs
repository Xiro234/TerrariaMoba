using Terraria.ModLoader;

namespace TerrariaMoba.Interfaces {
    public interface IDrawEffects {
        void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright);
    }
}