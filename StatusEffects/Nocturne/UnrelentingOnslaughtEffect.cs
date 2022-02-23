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
        
        public UnrelentingOnslaughtEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        public bool Shoot(ref Item item, ref ProjectileSource_Item_WithAmmo source, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback) {

            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                SoundEngine.PlaySound(SoundID.Item114, User.Center);
                float mult = 2f;
                Projectile.NewProjectile(new ProjectileSource_StatusEffect(User, this),User.Center + velocity, velocity, ProjectileID.LightBeam, (int) (damage * mult),
                    0, User.whoAmI);
            }

            return false;
        }
    }
}