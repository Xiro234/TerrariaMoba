using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Abilities;

namespace TerrariaMoba.Projectiles {
    public class ProjectileSource_Ability : IProjectileSource {
        public readonly Player Player;
        public readonly Ability Ability;

        public ProjectileSource_Ability(Player player, Ability ability) {
            Player = player;
            Ability = ability;
        }
    }
}