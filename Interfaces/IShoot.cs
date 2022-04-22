using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace TerrariaMoba.Interfaces {
    public interface IShoot : IAbilityEffectInterface {
        bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, 
            int damage, float knockback);
    }
}