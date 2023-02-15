using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Projectiles {
    public class EnititySource_StatusEffect : IEntitySource {
        public readonly Player Player;
        public readonly StatusEffect Effect;

        public string Context {
            get => Effect.DisplayName;
        }

        public EnititySource_StatusEffect(Player player, StatusEffect effect) {
            Player = player;
            Effect = effect;
        }
    }
}