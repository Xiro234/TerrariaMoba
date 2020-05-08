using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Utils;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles {
    public class SylviaUlt1Projectile : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt1");
        }
        
        public override void SetDefaults() {
            projectile.height = 20;
            projectile.width = 20;
            projectile.friendly = true;
            projectile.aiStyle = 0;
            projectile.tileCollide = true;
        }

        public override void AI() {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            for (int i = 0; i < 5; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Main.PlaySound(0, projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 3, 0,
                    0, 0, default(Color), 1f);
            }
            return true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit) {
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 3, 0,
                    0, 0, default(Color), 1f);
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 3, 0,
                    0, 0, default(Color), 1f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 3, 0,
                    0, 0, default(Color), 1f);
            }
        }
    }
}