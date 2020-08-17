using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles.Flibnob {
    public class FlameBelchSpawner : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
        }

        public override void AI() {
            // Flamethrower AI modified
            if (projectile.timeLeft > 60) {
                projectile.timeLeft = 60;
            }
            if (projectile.ai[0] > 2f) {
                var num297 = 1f;
                if (projectile.ai[0] == 8f) {
                    num297 = 0.25f;
                } else if (projectile.ai[0] == 9f) {
                    num297 = 0.5f;
                } else if (projectile.ai[0] == 10f) {
                    num297 = 0.75f;
                }
                projectile.ai[0] += 1f;
                var num298 = 6;
                if (num298 == 6 || Main.rand.Next(2) == 0) {
                    int num3;
                    for (var num299 = 0; num299 < 1; num299 = num3 + 1) {
                        var num300 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y),
                            projectile.width / 2, projectile.height / 2, num298, projectile.velocity.X * 0.2f,
                            projectile.velocity.Y * 0.2f, 100);
                        
                        Dust dust3;
                        if (Main.rand.Next(3) != 0 || num298 == 75 && Main.rand.Next(3) == 0) {
                            Main.dust[num300].noGravity = true;
                            dust3 = Main.dust[num300];
                            dust3.scale *= 3f;
                            var dust52 = Main.dust[num300];
                            dust52.velocity.X *= 2f;
                            var dust53 = Main.dust[num300];
                            dust53.velocity.Y *= 2f;
                        }

                        if (projectile.type == 188) {
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
                            dust3.velocity += projectile.velocity;
                            if (!Main.dust[num300].noGravity) {
                                dust3 = Main.dust[num300];
                                dust3.velocity *= 0.5f;
                            }
                        }

                        num3 = num299;
                    }
                }
            } else {
                projectile.ai[0] += 1f;
            }
            projectile.rotation += 0.3f * projectile.direction;
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            target.AddBuff(BuffID.OnFire, 105, false);
        }
    }
}