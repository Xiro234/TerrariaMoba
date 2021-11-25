using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class EclipteranLightbringer : Ability {
        public EclipteranLightbringer(Player player) : base(player, "Eclipteran Lightbringer", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Nocturne/NocturneUltimateTwo").Value; }

        public override void OnCast() {
            //TODO - Changes trait and abilities:
            //T = kills and assists also grant increased experience
            //A1 = can no longer switch, set to spear, on use spear inflict %health burn 
            //A2 = can move whilst active
            //A3 = base effects become passive whilst near nocturne and do opposite to enemies in range
        }
    }
}