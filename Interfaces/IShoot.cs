using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace TerrariaMoba.Interfaces {
    public interface IShoot : IAbilityEffectInterface {
        bool Shoot(ref Item item, ref EntitySource_ItemUse_WithAmmo source, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback);
    }
}