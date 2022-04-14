using Microsoft.Xna.Framework;
using Terraria;

namespace TerrariaMoba.Interfaces {
    public interface IModifyShootStats : IAbilityEffectInterface {
        void ModifyShootStats(ref Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback);
    }
}