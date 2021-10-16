using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Flibnob {
    class SplitEarth : ModProjectile {

        public int EarthDuration { get; set; }

        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults() {
            Projectile.Name = "Split Earth";
            Projectile.width = 158;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 40;
        }

        public override void AI() {
            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }

            if ((int)Projectile.ai[0] == 0) {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                for (int i = 0; i < 30; i++) {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, 0, 0, 0, default(Color), 1.2f);
                }
            }
            
            Projectile.ai[0] += 1f;

            if ((int)Projectile.ai[0] == EarthDuration) {
                Projectile.Kill();
            }
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, 0, 0, 0, default(Color), 1.2f);
            }
        }
    }
}