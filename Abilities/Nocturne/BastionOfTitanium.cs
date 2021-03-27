using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class BastionOfTitanium : Ability {
        public BastionOfTitanium() : base("Bastion of Titanium", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Titanium on armor expands; changes A1,2,3 and Trait:
            //T = Armor aura (+mr if in A2)
            //A1 = clones that dmg and slow (+atkspd slow if you cast whilst in A3)
            //A2 = slightly modified titanium shell (grant allies in A3 lesser reflect [1 proj, take 50% damage])
            //A3 = "evil" consecration like jorm; if allies effected they get bonuses
        }
    }
}