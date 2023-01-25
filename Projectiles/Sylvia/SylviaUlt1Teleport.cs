using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt1Teleport : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt1Teleport");
        }
        
        public override void SetDefaults() {
            Projectile.height = 48;
            Projectile.width = 30;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.tileCollide = true;
        }

        public override void AI() {
            if (Projectile.ai[0] == 0) {
                Player Player = Main.player[Projectile.owner];
                SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
                for (int i = 0; i < 20; i++) {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, 0,
                        0, 150, Color.LightGreen, 0.7f);
                }
            }

            Projectile.ai[0] += 1;

            if (Projectile.ai[0] == 20) {
                Projectile.Kill();
            }
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Projectile.velocity = Vector2.Zero;
            return true;
        }

        public override bool? CanCutTiles() {
            return false;
        }
    }
}