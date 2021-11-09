using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public class TitaniumShellEffect : StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Titanium Shell"; }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private int shellArmor;
        private int shellMagRes;
        private float moveSpeed;
        private float healDamage;
        private int mitigatedDamageTaken;

        public TitaniumShellEffect(int armor, int mr, float ms, float heal, int duration) : base(duration, true) {
            shellArmor = armor;
            shellMagRes = mr;
            moveSpeed = ms;
            healDamage = heal;
        }

        protected override Dictionary<AttributeType, Func<float>> FlatAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>> {
                { PHYSICAL_ARMOR, () => shellArmor },
                { PHYSICAL_ARMOR, () => shellMagRes }
            };
        }
        
        protected override Dictionary<AttributeType, Func<float>> MultAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MOVEMENT_SPEED, () => 1 - moveSpeed }
            };
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            //TODO - Store mitigated damage correctly.
            mitigatedDamageTaken = 69;
        }

        public override void FallOff() {
            User.statLife += (int) (mitigatedDamageTaken * healDamage);
        }
    }
}