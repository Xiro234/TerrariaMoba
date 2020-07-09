using Terraria;

namespace TerrariaMoba.Abilities.Osteo {
    public class RaiseDead : Ability {
        public RaiseDead(Player myPlayer) : base(myPlayer) {
            Name = "Raise Dead";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }
    }
}