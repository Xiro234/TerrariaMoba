using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class VerdantFuryEffect : StatusEffect, IShoot {
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
        
        public bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {
            speedX *= attackSpeed;
            speedY *= attackVelocity;
            return true;
        }
    }
}