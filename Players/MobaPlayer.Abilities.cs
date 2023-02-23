using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Network;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public void TickAbilities() {
            if (Hero != null) {
                for (int i = 0; i < Hero.Skills.Length; i++) {
                    Ability ability = Hero.Skills[i];
                    
                    if (ability?.IsActive == true) {
                        ability.WhileActive();
                    }
                    
                    ability?.TickCooldown();

                    if (ability?.NetUpdate == true && Player.whoAmI == Main.myPlayer && Main.netMode != NetmodeID.SinglePlayer) {
                        NetworkHandler.SendSyncAbility(i, Player.whoAmI, Main.myPlayer);
                        ability.NetUpdate = false;
                    }
                }
            }
        }
    }
}