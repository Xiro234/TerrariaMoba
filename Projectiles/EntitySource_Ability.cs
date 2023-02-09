using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Abilities;

namespace TerrariaMoba.Projectiles {
    public class EntitySource_Ability : IEntitySource {
        public readonly Player Player;
        public readonly Ability Ability;

        public string Context {
            get => Ability.Name;
        }

        public EntitySource_Ability(Player player, Ability ability) {
            Player = player;
            Ability = ability;
        }
    }
}