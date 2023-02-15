using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Jorm;

namespace TerrariaMoba.Projectiles.Jorm {
    public class ConsecrationProj : ModProjectile {
        public float ConsecSpread { get; set; }
        public float ConsecDuration { get; set; }

        public override void SetDefaults() {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 1000;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;

            ConsecSpread = Consecration.CONSEC_SPREAD_RANGE;
            ConsecDuration = Consecration.CONSEC_DURATION;
        }

        public override void AI() {
            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = (int) ConsecDuration;
            }

            Projectile.position.Y = GetYPos() - 2;
            
            if (Projectile.width < ConsecSpread) {
                Projectile.width += 5;
                Projectile.position.X -= 2.5f;
            }

            for (int d = 0; d < 20; d++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sandnado, 0, 0, 50);
            }

        }
        
        private int GetYPos() {
            int posX = (int)Projectile.Bottom.X;
            int posY = (int)Projectile.Bottom.Y;

            if (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                while (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY -= 1;
                }
            }
            else {
                while (!TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY += 1;
                }
            }
            return posY;
        }
    }
}