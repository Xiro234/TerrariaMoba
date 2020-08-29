using Terraria;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Sylvia {
    public class JunglesWrath : Ability {
        public JunglesWrath(Player myPlayer) : base(myPlayer) {
            Type = AbilityType.Passive;
            Name = "Jungle's Wrath";
            IsActive = true;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaTrait");
        }

        public override void Using() {
            IsActive = true;
        }
    }
}