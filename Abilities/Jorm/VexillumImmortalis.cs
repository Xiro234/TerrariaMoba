using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class VexillumImmortalis : Ability {
        public VexillumImmortalis() : base("Vexillum Immortalis", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public override void OnCast() {
            //TODO - Jorm summons a banner on top of him which reduces all physical damage taken by 100%. Can be destroyed.
        }
    }
}