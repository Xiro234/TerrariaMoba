using Terraria;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Interfaces; 

public interface IDealPvpDamage {
    void DealPvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, Player target, DamageSource damageSource);
}