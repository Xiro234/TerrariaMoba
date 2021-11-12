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
        public VersatileCombatant(Player player) : base(player, "Versatile Combatant", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        //TODO - Switch between 2 weapons, 2h sword and throwing spear, think jinx q
        //TODO - 2h sword has unrelenting onslaught effect, throwing spear has a stacking attack speed buff

        public override void OnCast() {
            /*
             * if primary weapon is sword
             * change weapon to spear
             * set issword to false;
             * set isspear to true
             * else
             * change weapon to sword
             * set isspear to false
             * set issword to true
             */
        }

        public override void WhileActive() {
            /*
             * if issword
             * every fifth attack deals %max health dmg
             * if isspear
             * every successful(?) attack increases attackspeed
             */
        }
    }
}