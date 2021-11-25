using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Sylvia {
    public class WitheredRose : Ability {
        public WitheredRose(Player player) : base(player, "Withered Rose", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaAbilityTwo").Value;  }

        public override void OnCast() {
            //TODO - new sylvia ability
            //passive: all magic damage has a chance to inflict a poison
            //toggle: surrounds sylvia in a thorn bush when damage is taken, fire a homing thorn that deals 50% of primary damage towards attacker as magic dmg, drains mana whilst on
        }
    }
}