using TerrariaMoba.Players;

namespace TerrariaMoba.Effects {
    public class EffectManager {
        public static int GetIndexOfEffect(Effect effect) {
            return effect.User.GetModPlayer<MobaPlayer>().effectList.FindIndex(e => e == effect);
        }
    }
}