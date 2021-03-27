using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrariaMoba.Abilities.Sylvia;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt2 : ModProjectile {
        
        public int SporeDamage { get; set; }
        public int NumberOfSpores { get; set; }
        public int SporeDuration { get; set; }
        
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt2");
        }
        
        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 0;
            drawOffsetX = -46;

            SporeDamage = PlanterasLastWill.SPORE_BASE_DAMAGE;
            NumberOfSpores = PlanterasLastWill.SPORE_BASE_NUMBER;
            SporeDuration = PlanterasLastWill.SPORE_BASE_DURATION;
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
                for (int i = 0; i < NumberOfSpores; i++) {
                    Vector2 velocity = Main.rand.NextVector2Unit();
                    velocity *= 4;

                    //TODO - Create new spore projectile and pass on SporeDuration.
                    //Projectile.NewProjectile(projectile.position, velocity, TerrariaMoba.Instance.ProjectileType("SylviaSpores"), SporeDamage, 0, projectile.owner);
                }
            }
        }
    }
}