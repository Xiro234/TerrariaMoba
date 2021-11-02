using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Marie {
    public class BlessingOfTheGoddess : Ability {
        public BlessingOfTheGoddess() : base("Blessing of the Goddess", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieUltimateOne").Value; }

        private int Timer;
        
        public override void OnCast() {
            Timer = 69;
            IsActive = true;
        }

        public override void WhileActive() {
            //TODO - Whilst allies are near marie, they gain buffs etc.
        }
    }
}