using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class BastionOfTitanium : Ability {
        public BastionOfTitanium() : base("Bastion of Titanium", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Changes trait and abilities:
            //T = kills and assists also create a untargetable clone of them that slows enemies then blows up to deal dmg (lissandra passive)
            //A1 = can no longer switch, set to sword, on use do something?
            //A2 = duration is increased by 100%
            //A3 = base effects become passive whilst near nocturne and do opposite to enemies in range
        }
    }
}