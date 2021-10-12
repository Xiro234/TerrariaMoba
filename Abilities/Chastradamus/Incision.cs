using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class Incision : Ability {
        public Incision() : base("Incision", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Chastradamus slashes his surgical blade with confidence, inflicting Ruptured for 3s.
        }
    }
}