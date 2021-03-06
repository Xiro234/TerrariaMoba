using Terraria.DataStructures;

namespace TerrariaMoba.Interfaces {
    public interface IKill : IAbilityEffectInterface {
        void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource);
    }
}