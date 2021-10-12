using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.UI {
    public static class UIUtils {
        public static Texture2D GetBarFromResource(Resource resource) {
            switch (resource) {
                case Resource.Life:
                    return ModContent.Request<Texture2D>("Textures/HealthBar").Value;
                case Resource.Mana:
                    return ModContent.Request<Texture2D>("Textures/ManaBar").Value;
                default:
                    return ModContent.Request<Texture2D>("Textures/HealthBar").Value;
            }
        }
    }
}