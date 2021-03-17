using Terraria.ModLoader;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public void TickAbilities() {
            foreach (var ability in TestAbilities) {
                if (ability.IsActive) {
                    ability.WhileActive();
                }
            }
        }
    }
}