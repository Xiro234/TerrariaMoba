using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.Abilities {
    public class TestShootAbility : Ability, IShoot {
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value;} }

        public TestShootAbility(Player player) : base(player, "TestShootAbility", 60, 0, AbilityType.Passive) {
            
        }

        public bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, 
            int damage, float knockback) {
            return true;
        }

        public override bool CanCastAbility() {
            return true;
        }
    }
}