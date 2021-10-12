using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class Crowstorm : Ability {
        public Crowstorm() : base("Crowstorm", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Chastradamus summons crows to feast on his enemies that inflict Crow’s Bite.
        }
    }
}