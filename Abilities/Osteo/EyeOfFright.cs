using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Osteo {
    public class EyeOfFright : Ability {
        public EyeOfFright(Player player) : base(player, "Eye of Fright", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoAbilityThree").Value; }

        public override void OnCast() {
            
        }
    }
}