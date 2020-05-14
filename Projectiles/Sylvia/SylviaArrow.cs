using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaArrow  : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaArrow");
        }

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.arrow = true;
            projectile.width = 8;
            projectile.height = 8;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.aiStyle = 1;
            drawOffsetX = -6;
            aiType = ProjectileID.WoodenArrowFriendly;
        }

        public override void Kill(int timeLeft) {
            Main.PlaySound(0, (int) projectile.position.X, (int) projectile.position.Y);
            for (int num737 = 0; num737 < 10; num737++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 7, 0f, 0f, 0, Color.Red, 1f);
            }
        }
    }
}