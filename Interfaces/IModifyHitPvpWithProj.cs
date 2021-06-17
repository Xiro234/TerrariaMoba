using Terraria;

namespace TerrariaMoba.Interfaces {
    public interface IModifyHitPvpWithProj : IAbilityEffectInterface {
        void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit);
    }
}