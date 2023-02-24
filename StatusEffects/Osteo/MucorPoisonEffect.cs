using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects.GenericEffects;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Osteo {
    public class MucorPoisonEffect : DamageOverTime {
        public override string DisplayName { get => "Mucormycosis Poison"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value; } }

        public MucorPoisonEffect() { }
        public MucorPoisonEffect(int dmg, int duration, bool canBeCleansed, int applierId) : base(0, dmg, 0, duration, canBeCleansed, applierId) { }

        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { HEALTH_REGEN, () => 0 }
            };
        }
    }
}
