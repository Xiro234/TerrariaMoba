using Terraria;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Osteo {
    public class SkeletalBond : Ability {
        public SkeletalBond(Player myPlayer) : base(myPlayer) {
            Type = AbilityType.Passive;
            Name = "Skeletal Remains";
            IsActive = true;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }
    }
}