using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class PaladinsResolve : Ability {
        public PaladinsResolve() : base("Paladin's Resolve", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void WhileActive() {
            //TODO - hephaestan might, 4 stacks = empowered ability | 10 armor, if nearest ally is below 25%hp, take 15% of damage they take.
        }
    }
}