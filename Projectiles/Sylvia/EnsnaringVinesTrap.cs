using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class EnsnaringVinesTrap : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("EnsnaringVines");
        }
        
        public int TrapDuration { get; set; }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 34;
            Projectile.height = 26;
            Projectile.timeLeft = 1000;
            Projectile.tileCollide = false;

            TrapDuration = EnsnaringVines.TRAP_DURATION;
        }
        
        public override void AI() {
            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = TrapDuration;
            }

            if ((int)Projectile.ai[0] == 0) {
                SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
                for (int i = 0; i < 20; i++) {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, 0,
                        0, 150, Color.LightGreen, 0.7f);
                }
                Projectile.ai[0] += 1f;
            }
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
        }
     }
}