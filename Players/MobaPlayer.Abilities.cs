using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public void TickAbilities() {
            if (Hero != null) {
                foreach (var ability in Hero.Skills) {
                    if (ability?.IsActive == true) {
                        ability.WhileActive();
                    }
                    
                    ability?.TickCooldown();
                }
            }
        }
    }
}