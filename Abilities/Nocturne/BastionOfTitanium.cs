using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class BastionOfTitanium : Ability {
        public BastionOfTitanium() : base("Bastion of Titanium", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Titanium on armor expands; changes A1,2,3 and Trait:
            //T = An aura of Armor (if A2 is active, this also grants MR)
            //A1 = Creates an illusion of titanium behind all enemies. Max HP = 25% of enemy Max HP. Damage dealt = BAttack Damage of enemy.
            //    Inflicts .
            //A2 = Parry attacks for 2.5 seconds. Slowed by 25%. If hit, after 2.5s, Trait aura Silences enemies in radius.
            //A3 = "evil" consecration like jorm; if allies effected they get bonuses
        }
    }
}