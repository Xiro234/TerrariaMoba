using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class HolyBarrier : StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Holy Barrier"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float rangeToAbsorb;
        private float absorbMagnitude;

        public HolyBarrier() { }

        public HolyBarrier(float range, float magnitude, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            rangeToAbsorb = range;
            absorbMagnitude = magnitude;
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            Player jormPlayer = Main.player[ApplicantID];
            float dist = (jormPlayer.Center - User.Center).Length() / 16f;
            if (jormPlayer.active && dist <= rangeToAbsorb) {
                int jormPhysTaken = physicalDamage - (int)(physicalDamage * absorbMagnitude);
                int jormMagTaken = magicalDamage - (int)(magicalDamage * absorbMagnitude);
                int jormTrueTaken = trueDamage - (int)(trueDamage * absorbMagnitude);
                jormPlayer.GetModPlayer<MobaPlayer>().TakePvpDamage(jormPhysTaken, jormMagTaken, jormTrueTaken, killer, true);

                physicalDamage = (int)(physicalDamage * absorbMagnitude);
                magicalDamage = (int)(magicalDamage * absorbMagnitude);
                trueDamage = (int)(trueDamage * absorbMagnitude);
            }
        }
    }
}