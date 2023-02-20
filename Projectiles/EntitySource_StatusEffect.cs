using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Projectiles {
    public class EntitySource_StatusEffect : IEntitySource {
        public readonly Player Player;
        public readonly StatusEffect Effect;

        public string Context {
            get => Effect.DisplayName;
        }

        public EntitySource_StatusEffect(Player player, StatusEffect effect) {
            Player = player;
            Effect = effect;
        }
    }
}