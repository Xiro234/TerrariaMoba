using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;

namespace TerrariaMoba.Interfaces {
    public class AbilityEffectManager {
        public static void Kill(Player player, double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            List<Ability> abilities = GetValidAbilities<IKill>(player);
            //List<StatusEffect> statusEffects = GetValidStatusEffects<IKill>(player);

            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    ((IKill)ability).Kill(damage, hitDirection, pvp, damageSource);
                }
            }
        }

        public static bool Shoot(Player player, Item item, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack) {
            List<Ability> abilities = GetValidAbilities<IShoot>(player);
            //List<StatusEffect> statusEffects = GetValidStatusEffects<IKill>(player);

            bool result = true;
            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    Main.NewText("woo");
                    result &= ((IShoot)ability).Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
                }
            }

            return result;
        }

        public static List<Ability> GetValidAbilities<T>(Player player) {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            List<Ability> abilities = new List<Ability>();

            for(int i = 0; i < mobaPlayer.TestAbilities.Count; i++) {
                if (mobaPlayer.TestAbilities[i] is T) {
                    abilities.Add(mobaPlayer.TestAbilities[i]);
                }
            }

            return abilities;
        }
    }
}