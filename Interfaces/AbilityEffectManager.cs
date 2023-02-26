using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Interfaces {
    public class AbilityEffectManager {
        #region Hooks
        public static void Kill(Player Player, double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            List<Ability> abilities = GetValidAbilities<IKill>(Player);
            List<StatusEffect> effects = GetValidEffects<IKill>(Player);
            
            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    ((IKill)ability).Kill(damage, hitDirection, pvp, damageSource);
                }
            }
            
            foreach (StatusEffect effect in effects) {
                ((IKill)effect).Kill(damage, hitDirection, pvp, damageSource);
            }
        }

        public static bool PreKill(Player Player, double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            List<Ability> abilities = GetValidAbilities<IPreKill>(Player);
            List<StatusEffect> effects = GetValidEffects<IPreKill>(Player);

            bool result = true;
            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    result &= ((IPreKill)ability).PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
                }
            }

            foreach (StatusEffect effect in effects) {
                result &= ((IPreKill)effect).PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
            }

            return result;
        }

        public static bool Shoot(Player Player, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, 
            Vector2 velocity, int type, int damage, float knockback) {
            List<Ability> abilities = GetValidAbilities<IShoot>(Player);
            List<StatusEffect> effects = GetValidEffects<IShoot>(Player);

            bool result = true;
            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    result &= ((IShoot)ability).Shoot(item, source, position, velocity, type, damage, knockback);
                }
            }
            
            foreach (StatusEffect effect in effects) {
                result &= ((IShoot)effect).Shoot(item, source, position, velocity, type, damage, knockback);
            }
            
            return result;
        }

        public static float UseSpeedMultiplier(Player player, ref Item item) {
            List<Ability> abilities = GetValidAbilities<IUseSpeedMultiplier>(player);
            List<StatusEffect> effects = GetValidEffects<IUseSpeedMultiplier>(player);

            float result = 1f;
            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    result *= ((IUseSpeedMultiplier)ability).UseSpeedMultiplier(ref item);
                }
            }
            
            foreach (StatusEffect effect in effects) {
                result *= ((IUseSpeedMultiplier)effect).UseSpeedMultiplier(ref item);
            }

            return result;
        }

        public static void ModifyShootStats(Player player, ref Item item, ref Vector2 position, ref Vector2 velocity,
            ref int type, ref int damage, ref float knockback) {
            List<Ability> abilities = GetValidAbilities<IModifyShootStats>(player);
            List<StatusEffect> effects = GetValidEffects<IModifyShootStats>(player);

            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    ((IModifyShootStats)ability).ModifyShootStats(ref item, ref position, ref velocity, ref type, ref damage, ref knockback);
                }
            }
            
            foreach (StatusEffect effect in effects) {
                ((IModifyShootStats)effect).ModifyShootStats(ref item, ref position, ref velocity, ref type, ref damage, ref knockback);
            }
        }

        public static void SetControls(Player Player) {
            List<Ability> abilities = GetValidAbilities<ISetControls>(Player);
            List<StatusEffect> effects = GetValidEffects<ISetControls>(Player);

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

        public static void DrawEffects(Player Player, PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a,
            ref bool fullBright) {
            List<Ability> abilities = GetValidAbilities<IDrawEffects>(Player);
            List<StatusEffect> effects = GetValidEffects<IDrawEffects>(Player);

            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    ((IDrawEffects)ability).DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
                }
            }

            foreach (StatusEffect effect in effects) {
                ((IDrawEffects)effect).DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
            }
        }

        public static void DealPvpDamage(Player Player, ref int physicalDamage, ref int magicalDamage, ref int trueDamage,
            Player target, DamageSource damageSource) {
            List<Ability> abilities = GetValidAbilities<IDealPvpDamage>(Player);
            List<StatusEffect> effects = GetValidEffects<IDealPvpDamage>(Player);

            foreach (Ability ability in abilities) {
                if (ability.CanCastAbility()) {
                    ((IDealPvpDamage)ability).DealPvpDamage(ref physicalDamage, ref magicalDamage, ref trueDamage, target, damageSource);
                }
            }

            foreach (StatusEffect effect in effects) {
                ((IDealPvpDamage)effect).DealPvpDamage(ref physicalDamage, ref magicalDamage, ref trueDamage, target, damageSource);
            }
        }

        public static void ResetEffects(Player Player) {
            List<Ability> abilities = GetValidAbilities<IResetEffects>(Player);
            List<StatusEffect> effects = GetValidEffects<IResetEffects>(Player);

            foreach (Ability ability in abilities) {
                ((IResetEffects) ability).ResetEffects();
            }

            foreach (StatusEffect effect in effects) {
                ((IResetEffects)effect).ResetEffects();
            }
        }

        public static void TakePvpDamage(Player Player, ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            List<Ability> abilities = GetValidAbilities<ITakePvpDamage>(Player);
            List<StatusEffect> effects = GetValidEffects<ITakePvpDamage>(Player);
            //TODO - Add some sort of system to make sure multiple effects will always make a consistent outcome (I.E, 2x damage + 2x damage + flat damage vs., 2x damage + flat damage + 2x damage)
            foreach (Ability ability in abilities) {
                ((ITakePvpDamage)ability).TakePvpDamage(ref physicalDamage, ref magicalDamage, ref trueDamage, ref killer);
            }

            foreach (StatusEffect effect in effects) {
                ((ITakePvpDamage)effect).TakePvpDamage(ref physicalDamage, ref magicalDamage, ref trueDamage, ref killer);
            }
        }

        public static void OnHeal(Player Player, ref int amount, ref bool doText) {
            List<Ability> abilities = GetValidAbilities<IOnHeal>(Player);
            List<StatusEffect> effects = GetValidEffects<IOnHeal>(Player);

            bool curDoText = true;
            
            foreach (Ability ability in abilities) {
                ((IOnHeal)ability).OnHeal(ref amount, ref curDoText);
                doText = doText && curDoText;
            }
            
            foreach (StatusEffect effect in effects) {
                ((IOnHeal)effect).OnHeal(ref amount, ref doText);
                doText = doText && curDoText;
            }
        }

        public static void OnHealOtherPlayer(Player Player, Player target, ref int amount, ref bool doText) {
            List<Ability> abilities = GetValidAbilities<IOnHealOtherPlayer>(Player);
            List<StatusEffect> effects = GetValidEffects<IOnHealOtherPlayer>(Player);

            bool curDoText = true;
            
            foreach (Ability ability in abilities) {
                ((IOnHealOtherPlayer)ability).OnHealOtherPlayer(target, ref amount, ref curDoText);
                doText = doText && curDoText;
            }
            
            foreach (StatusEffect effect in effects) {
                ((IOnHealOtherPlayer)effect).OnHealOtherPlayer(target, ref amount, ref curDoText);
                doText = doText && curDoText;
            }
        }

        public static void OnCast(Player Player, Ability castAbility) {
            List<Ability> abilities = GetValidAbilities<IOnCast>(Player);
            List<StatusEffect> effects = GetValidEffects<IOnCast>(Player);
            
            foreach (Ability ability in abilities) {
                ((IOnCast)ability).OnCast(castAbility);
            }
            
            foreach (StatusEffect effect in effects) {
                ((IOnCast)effect).OnCast(castAbility);
            }
        }

        public static bool OnRespawn(Player Player) {
            List<Ability> abilities = GetValidAbilities<IOnRespawn>(Player);
            List<StatusEffect> effects = GetValidEffects<IOnRespawn>(Player);

            bool result = true;
            foreach (Ability ability in abilities) {
                result &= ((IOnRespawn)ability).OnRespawn();
            }

            foreach (StatusEffect effect in effects) {
                result &= ((IOnRespawn)effect).OnRespawn();
            }

            return result;
        }
        #endregion
        
        
        public static List<Ability> GetValidAbilities<T>(Player Player) {
            var mobaPlayer = Player.GetModPlayer<MobaPlayer>();
            List<Ability> abilities = new List<Ability>();

            for (int i = 0; i < mobaPlayer.Hero?.Skills.Length; i++) {
                if (mobaPlayer.Hero.Skills[i] is T) {
                    abilities.Add(mobaPlayer.Hero.Skills[i]);
                }
            }

            return abilities;
        }

        public static List<StatusEffect> GetValidEffects<T>(Player Player) {
            var mobaPlayer = Player.GetModPlayer<MobaPlayer>();
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