using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles;

namespace TerrariaMoba.Abilities.Nocturne {
    public class VersatileCombatant : Ability {
        public VersatileCombatant() : base("Versatile Combatant", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        //TODO - Switch between 2 weapons, 2h sword and throwing spear, think jinx q
        //TODO - 2h sword has unrelenting onslaught effect, throwing spear has a stacking attack speed buff
        
        
    }
}