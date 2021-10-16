using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaArrow  : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaArrow");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 1;
            DrawOffsetX = -6;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(0, (int) Projectile.position.X, (int) Projectile.position.Y);
            for (int i = 0; i < 10; i++) {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 7, 0f, 0f, 0, Color.Red, 1f);
            }
        }
    }
}