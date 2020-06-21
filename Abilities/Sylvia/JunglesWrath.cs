using Terraria;
using System;
using IL.Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.Abilities.Sylvia {
    public class JunglesWrath : Ability {
        public JunglesWrath(Player myPlayer) : base(myPlayer) {
            Type = AbilityType.Passive;
            Name = "Jungle's Wrath";
            Active = true;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaTrait");
        }

        public override void InUse() {
            Active = true;
        }
    }
}