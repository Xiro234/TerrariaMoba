using Terraria;

namespace TerrariaMoba.Interfaces {
    public interface IModifyHitPvpWithProj : IAbilityEffectInterface {
        void ModifyHitPvpWithProj(Projectile proj, Player target, ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref bool crit);
    }
}