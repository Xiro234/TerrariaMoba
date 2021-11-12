using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public void TickAbilities() {
            if (Hero != null) {
                foreach (var ability in Hero.Skills) {
                    if (ability == null) {
                        continue;
                    }
                    
                    if (ability.IsActive) {
                        ability.WhileActive();
                    }

                    if (ability.CooldownTimer > 0) {
                        ability.CooldownTimer--;
                    } //TODO - TEMPORARY, WILL CHANGE TO AN ABILITY-TO-ABILITY BASIS
                }
            }
        }
    }
}