using System.Collections.Generic;
using Terraria.ModLoader;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public List<StatusEffect> EffectList { get; private set; }

        public void TickStatusEffects() {
            for (int i = EffectList.Count - 1; i >= 0; i--) {
                var effect = EffectList[i];
                effect.WhileActive();
                effect.CheckSync();
                if (effect.DurationTimer <= 0) {
                    effect.FallOff();
                    EffectList.RemoveAt(i);
                }
            }
        }
    }
}