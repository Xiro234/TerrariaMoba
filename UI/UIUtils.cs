using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.UI {
    public static class UIUtils {
        public static Texture2D GetBarFromResource(Resource resource) {
            switch (resource) {
                case Resource.Life:
                    return ModContent.Request<Texture2D>("TerrariaMoba/Textures/HealthBar", AssetRequestMode.ImmediateLoad).Value;
                case Resource.Mana:
                    return ModContent.Request<Texture2D>("TerrariaMoba/Textures/ManaBar", AssetRequestMode.ImmediateLoad).Value;
                default:
                    return ModContent.Request<Texture2D>("TerrariaMoba/Textures/HealthBar", AssetRequestMode.ImmediateLoad).Value;
            }
        }
    }
}