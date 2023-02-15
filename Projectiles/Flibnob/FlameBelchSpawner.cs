﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Flibnob {
    public class FlameBelchSpawner : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
        }

        public override void AI() {
            // Flamethrower AI modified
            if (Projectile.timeLeft > 60) {
                Projectile.timeLeft = 60;
            }
            if (Projectile.ai[0] > 2f) {
                var num297 = 1f;
                if (Projectile.ai[0] == 8f) {
                    num297 = 0.25f;
                } else if (Projectile.ai[0] == 9f) {
                    num297 = 0.5f;
                } else if (Projectile.ai[0] == 10f) {
                    num297 = 0.75f;
                }
                Projectile.ai[0] += 1f;
                var num298 = 6;
                if (num298 == 6 || Main.rand.NextBool(2)) {
                    int num3;
                    for (var num299 = 0; num299 < 1; num299 = num3 + 1) {
                        var num300 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y),
                            Projectile.width / 2, Projectile.height / 2, num298, Projectile.velocity.X * 0.2f,
                            Projectile.velocity.Y * 0.2f, 100);
                        
                        Dust dust3;
                        if (!Main.rand.NextBool(3) || num298 == 75 && Main.rand.NextBool(3)) {
                            Main.dust[num300].noGravity = true;
                            dust3 = Main.dust[num300];
                            dust3.scale *= 3f;
                            var dust52 = Main.dust[num300];
                            dust52.velocity.X *= 2f;
                            var dust53 = Main.dust[num300];
                            dust53.velocity.Y *= 2f;
                        }

                        if (Projectile.type == 188) {
                            dust3 = Main.dust[num300];
                            dust3.scale *= 1.25f;
                        } else {
                            dust3 = Main.dust[num300];
                            dust3.scale *= 1.5f;
                        }

                        var dust54 = Main.dust[num300];
                        dust54.velocity.X *= 1.2f;
                        var dust55 = Main.dust[num300];
                        dust55.velocity.Y *= 1.2f;
                        dust3 = Main.dust[num300];
                        dust3.scale *= num297;
                        
                        if (num298 == 75) {
                            dust3 = Main.dust[num300];
                            dust3.velocity += Projectile.velocity;
                            if (!Main.dust[num300].noGravity) {
                                dust3 = Main.dust[num300];
                                dust3.velocity *= 0.5f;
                            }
                        }

                        num3 = num299;
                    }
                }
            } else {
                Projectile.ai[0] += 1f;
            }
            Projectile.rotation += 0.3f * Projectile.direction;
        }
    }
}