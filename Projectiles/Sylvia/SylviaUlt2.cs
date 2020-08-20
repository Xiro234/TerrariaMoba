﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt2 : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt2");
        }
        
        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 0;
            drawOffsetX = -46;
        }

        public override void AI() {
            projectile.ai[0] += 1f;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            
            if (projectile.ai[0] < 15) {
                projectile.alpha = (int) ((255 / 15) * (15 - projectile.ai[0]));
            }
            else {
                projectile.alpha = 0;
            }

            Vector2 direction = Vector2.Normalize(projectile.velocity);

            for (int i = 0; i < 3; i++) {
                float val = (float) Math.Sin(6 * MathHelper.ToRadians(projectile.ai[0]) + (i / 2));
                Vector2 position1 = projectile.Center + (new Vector2(-direction.Y, direction.X) * (val * 30));
                Vector2 position2 = projectile.Center + (new Vector2(direction.Y, -direction.X) * (val * 30));

                Dust.NewDustPerfect(position1 - (direction * 100), 131, Vector2.Zero, projectile.alpha, Color.ForestGreen, 1f);
                Dust.NewDustPerfect(position2 - (direction * 100), 131, Vector2.Zero, projectile.alpha, Color.ForestGreen, 1f);
            }
        }

        public override void Kill(int timeLeft) {
            if (Main.myPlayer == projectile.owner) {
                Player player = Main.player[projectile.owner];
                int numProjectiles = 6;
                for (int i = 0; i < numProjectiles; i++) {
                    Vector2 velocity = Main.rand.NextVector2Unit();
                    velocity *= 4;

                    Projectile.NewProjectile(projectile.position, velocity,
                        TerrariaMoba.Instance.ProjectileType("SylviaSpores"), (int)player.GetModPlayer<MobaPlayer>().SylviaStats.U2SporeDmg.Value, 0, projectile.owner);
                }
            }
        }
    }
}