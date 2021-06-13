using Terraria.DataStructures;

namespace TerrariaMoba.Interfaces {
    public interface IPreHurt : IAbilityEffectInterface {
        bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource);
    }
}