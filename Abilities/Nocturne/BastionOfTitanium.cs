using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class BastionOfTitanium : Ability {
        public BastionOfTitanium() : base("Bastion of Titanium", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Titanium on armor expands; changes A1,2,3 and Trait:
            //T = An aura of Armor (if A2 is active, this also grants MR)
            //A1 = Creates an illusion of titanium behind all enemies that deal damage and slow on hit.
            //A2 = Parry attacks for 1 second. If hit, increase physical damage by 15% for 5 seconds, and auto-attacks inflict armor break.
            //A3 = "evil" consecration like jorm; if allies effected they get bonuses
        }
    }
}