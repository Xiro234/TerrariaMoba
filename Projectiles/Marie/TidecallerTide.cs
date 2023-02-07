using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Marie;

namespace TerrariaMoba.Projectiles.Marie {
    public class TidecallerTide : ModProjectile {
        public int TideDuration { get; set; }

        public override void SetDefaults() {
            Projectile.width = 162;
            Projectile.height = 42;
            Projectile.timeLeft = 1000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;

            TideDuration = Tidecaller.TIDE_DURATION;
        }

        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void AI() {
            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = TideDuration;
            }

            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(TideDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            TideDuration = reader.ReadInt32();
        }
    }
}
