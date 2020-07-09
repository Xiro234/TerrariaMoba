using Terraria;

namespace TerrariaMoba.Abilities.Osteo {
    public class SongOfTheDamned : Ability {
        public SongOfTheDamned(Player myPlayer) : base(myPlayer) {
            Name = "Raise Dead";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }
    }
}