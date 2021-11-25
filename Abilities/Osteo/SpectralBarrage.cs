using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Osteo {
    public class SpectralBarrage : Ability {
        public SpectralBarrage(Player player) : base(player, "Spectral Barrage", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoAbilityThree").Value; }

        public override void OnCast() {
            //TODO - toggle that fires homing spectral bolts every interval, draining mana along the way, heals on hit for 25% of damage dealt
        }
    }
}