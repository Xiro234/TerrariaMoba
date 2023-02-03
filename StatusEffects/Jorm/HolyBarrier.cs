using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Jorm {
    public class HolyBarrier : StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Holy Barrier"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private float jormArmor;
        private float jormMagRes;
        private float dmgAbsorbMag;

        public HolyBarrier() { }

        public HolyBarrier(float armor, float magres, float magnitude, int id, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            jormArmor = armor;
            jormMagRes = magres;
            dmgAbsorbMag = magnitude;
        }

        //TODO - Implement Jorm's damage absorption.
        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            /*
             * reduce phys dmg by *% and that *% jorm takes (premitigated)
             * reduce mag dmg by *% and that *% jorm takes
             * reduce true dmg by *% and you know the drill
             * dmgAbsorbMag determines actual percentage
             * TakeDamage(id, phys, mag, true)
             */
        }
    }
}