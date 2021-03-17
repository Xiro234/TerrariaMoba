using Terraria;

namespace TerrariaMoba.Interfaces {
    public interface IModifyHitPvpWithProj {
        void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit);
    }
}