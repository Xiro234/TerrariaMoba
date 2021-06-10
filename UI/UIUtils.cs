using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.UI {
    public static class UIUtils {
        public static Texture2D GetBarFromResource(Resource resource) {
            switch (resource) {
                case Resource.Life:
                    return TerrariaMoba.Instance.GetTexture("Textures/HealthBar");
                case Resource.Mana:
                    return TerrariaMoba.Instance.GetTexture("Textures/ManaBar");
                default:
                    return TerrariaMoba.Instance.GetTexture("Textures/HealthBar");
            }
        }
    }
}