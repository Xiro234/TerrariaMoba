using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
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
        
        public bool Shoot(ref Item item, ref ProjectileSource_Item_WithAmmo source, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback) {
            /*speedX *= attackSpeed;
            speedY *= attackVelocity;*/
            //TODO: Fix speed
            return true;
        }
    }
}