using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class Bloodletting : Ability {
        public Bloodletting(Player player) : base(player, "Bloodletting", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void WhileActive() {
            //TODO - Chastradamus gathers blood from dealing damage to enemies, at different stages, gives him buffs, but is consumed by A3 to heal him. Lose on death.
        }
    }
}