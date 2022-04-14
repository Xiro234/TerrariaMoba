using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class VerdantFuryEffect : StatusEffect, IUseSpeedMultiplier, IModifyShootStats {
        public override string DisplayName { get => "Verdant Fury"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private float attackSpeed;
        private float attackVelocity;
        
        public VerdantFuryEffect() { }

        public VerdantFuryEffect(int duration, float atkspd, float atkvel, bool canBeCleansed) : base(duration,
            canBeCleansed) {
            attackSpeed = atkspd;
            attackVelocity = atkvel;
        }

        public float UseSpeedMultiplier(ref Item item) {
            return attackSpeed;
        }

        public void ModifyShootStats(ref Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback) {
            velocity.Normalize();
            velocity *= User.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.ATTACK_VELOCITY) * attackVelocity;
        }
    }
}