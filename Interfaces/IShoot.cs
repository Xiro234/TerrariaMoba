using Microsoft.Xna.Framework;
using Terraria;

namespace TerrariaMoba.Interfaces {
    public interface IShoot {
        bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack);
    }
}