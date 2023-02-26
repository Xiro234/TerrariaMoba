using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.StatusEffects.Marie {
    public class MarieTraitEffect : StatusEffect, IDealPvpDamage {
        public override string DisplayName { get => "Goddess's Blessing"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value; } }

        private int damageIncrease;

        public MarieTraitEffect() { }
        public MarieTraitEffect(int magicDamage, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { 
            damageIncrease = magicDamage;
        }

        public void DealPvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, Player target, DamageSource damageSource) {
            magicalDamage += damageIncrease;
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(damageIncrease);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            damageIncrease = reader.ReadInt32();
            base.ReceiveEffectElements(reader);
        }
    }
}
