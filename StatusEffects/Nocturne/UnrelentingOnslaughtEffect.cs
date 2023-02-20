using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.StatusEffects.Nocturne {
    public class UnrelentingOnslaughtEffect: StatusEffect, IShoot {
        public override string DisplayName { get => "Unrelenting Onslaught"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }
        
        public UnrelentingOnslaughtEffect() { }
        
        public UnrelentingOnslaughtEffect(int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
        
        public bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, 
            int damage, float knockback) {

            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                SoundEngine.PlaySound(SoundID.Item114, User.Center);
                float mult = 2f;
                Projectile.NewProjectile(new EntitySource_StatusEffect(User, this),User.Center + velocity, velocity, ProjectileID.LightBeam, (int) (damage * mult),
                    0, User.whoAmI);
            }

            return false;
        }
    }
}