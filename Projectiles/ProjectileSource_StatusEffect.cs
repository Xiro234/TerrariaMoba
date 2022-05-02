using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Abilities;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Projectiles {
    public class ProjectileSource_StatusEffect : IEntitySource {
        public readonly Player Player;
        public readonly StatusEffect Effect;

        public string Context {
            get => Effect.DisplayName;
        }

        public ProjectileSource_StatusEffect(Player player, StatusEffect effect) {
            Player = player;
            Effect = effect;
        }
    }
}