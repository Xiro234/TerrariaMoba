using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.Players;

namespace TerrariaMoba.Interfaces {
    public class AbilityEffectManager {
        #region Hooks
        public static void Kill(Player player, double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            List<Ability> abilities = GetValidAbilities<IKill>(player);
            List<StatusEffect> effects = GetValidEffects<IKill>(player);
            
            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    ((IKill)ability).Kill(damage, hitDirection, pvp, damageSource);
                }
            }
            
            foreach (StatusEffect effect in effects) {
                ((IKill)effect).Kill(damage, hitDirection, pvp, damageSource);
            }
        }

        public static bool Shoot(Player player, Item item, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack) {
            List<Ability> abilities = GetValidAbilities<IShoot>(player);
            List<StatusEffect> effects = GetValidEffects<IShoot>(player);

            bool result = true;
            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    result &= ((IShoot)ability).Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
                }
            }
            
            foreach (StatusEffect effect in effects) {
                result &= ((IShoot)effect).Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            }

            return result;
        }

        public static void SetControls(Player player) {
            List<Ability> abilities = GetValidAbilities<ISetControls>(player);
            List<StatusEffect> effects = GetValidEffects<ISetControls>(player);

            bool result = true;
            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    ((ISetControls)ability).SetControls();
                }
            }

            foreach (StatusEffect effect in effects) {
                ((ISetControls)effect).SetControls();
            }
        }

        public static void DrawEffects(Player player, PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a,
            ref bool fullBright) {
            List<Ability> abilities = GetValidAbilities<IDrawEffects>(player);
            List<StatusEffect> effects = GetValidEffects<IDrawEffects>(player);

            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    ((IDrawEffects)ability).DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
                }
            }

            foreach (StatusEffect effect in effects) {
                ((IDrawEffects)effect).DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
            }
        }

        public static void ModifyHitPvpWithProj(Player player, Projectile proj, Player target, ref int damage, ref bool crit) {
            List<Ability> abilities = GetValidAbilities<IModifyHitPvpWithProj>(player);
            List<StatusEffect> effects = GetValidEffects<IModifyHitPvpWithProj>(player);

            foreach (Ability ability in abilities) {
                ((IModifyHitPvpWithProj)ability).ModifyHitPvpWithProj(proj, target, ref damage, ref crit);
            }

            foreach (StatusEffect effect in effects) {
                ((IModifyHitPvpWithProj)effect).ModifyHitPvpWithProj(proj, target, ref damage, ref crit);
            }
        }

        public static void ResetEffects(Player player) {
            List<Ability> abilities = GetValidAbilities<IResetEffects>(player);
            List<StatusEffect> effects = GetValidEffects<IResetEffects>(player);

            foreach (Ability ability in abilities) {
                ((IResetEffects) ability).ResetEffects();
            }

            foreach (StatusEffect effect in effects) {
                ((IResetEffects)effect).ResetEffects();
            }
        }

        public static void TakePvpDamage(Player player, ref int damage, ref int killer) {
            List<Ability> abilities = GetValidAbilities<ITakePvpDamage>(player);
            List<StatusEffect> effects = GetValidEffects<ITakePvpDamage>(player);
            //TODO - Add some sort of system to make sure multiple effects will always make a consistent outcome (I.E, 2x damage + 2x damage + flat damage vs., 2x damage + flat damage + 2x damage)
            foreach (Ability ability in abilities) {
                ((ITakePvpDamage) ability).TakePvpDamage(ref damage, ref killer);
            }

            foreach (StatusEffect effect in effects) {
                ((ITakePvpDamage)effect).TakePvpDamage(ref damage, ref killer);
            }
        }
        #endregion
        
        
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

        public static List<StatusEffect> GetValidEffects<T>(Player player) {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            List<StatusEffect> effects = new List<StatusEffect>();

            for(int i = 0; i < mobaPlayer.EffectList.Count; i++) {
                if (mobaPlayer.EffectList[i] is T) {
                    effects.Add(mobaPlayer.EffectList[i]);
                }
            }

            return effects;
        }
    }
}