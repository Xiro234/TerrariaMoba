using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class Bloodletting : Ability {
        public Bloodletting() : base("Bloodletting", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void WhileActive() {
            //TODO - Chastradamus gathers blood from dealing damage to enemies, at different stages, gives him buffs, but is consumed by A3 to heal him. Lose on death.
        }
    }
}