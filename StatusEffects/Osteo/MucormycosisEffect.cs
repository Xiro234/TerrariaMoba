using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace TerrariaMoba.StatusEffects.Osteo {
    public class MucormycosisEffect : StatusEffect {
        public override string DisplayName { get => "Fungal Armor"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value; } }

        private int MucorSporeDamage;
        private int MucorSporeDuration;
        private int MucorPoisonDamage;
        private int MucorPoisonDuration;

        public MucormycosisEffect() { }
        public MucormycosisEffect(int msdmg, int msdur, int mpdmg, int mpdur, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            MucorSporeDamage = msdmg;
            MucorPoisonDuration = msdur;
            MucorPoisonDamage = mpdmg;
            MucorPoisonDuration = mpdur;
        }

        public override void WhileActive() {
            //if player dies, spawn a projectile that inflicts poison

            base.WhileActive();
        }
    }
}
