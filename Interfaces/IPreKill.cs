using Terraria.DataStructures;

namespace TerrariaMoba.Interfaces {
    public interface IPreKill : IAbilityEffectInterface {
        bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource);
    }
}
