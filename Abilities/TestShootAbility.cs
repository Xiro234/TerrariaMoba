using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.Abilities {
    public class TestShootAbility : Ability, IShoot {
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }

        public TestShootAbility() : base("TestShootAbility", 60, 0, AbilityType.Passive) {
            
        }

        public bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {
            return true;
        }

        public override bool CanCastAbility() {
            return true;
        }
    }
}