﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaArrow  : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Adamantite Arrow");
        }

        public override void SetDefaults() {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1;
            DrawOffsetX = -6;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.arrow = true;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 10; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WoodFurniture, 0f, 0f, 0, Color.Red, 1f);
            }
        }
    }
}