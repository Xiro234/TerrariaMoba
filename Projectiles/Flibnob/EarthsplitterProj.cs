using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Flibnob {
    class EarthsplitterProj : ModProjectile {

        public int EarthDuration { get; set; }

        public override void SetStaticDefaults() {
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults() {
            projectile.Name = "Split Earth";
            projectile.width = 158;
            projectile.height = 42;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 40;
        }

        public override void AI() {
            if (++projectile.frameCounter >= 5) {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }

            if ((int)projectile.ai[0] == 0) {
                Main.PlaySound(SoundID.Dig, projectile.position);
                for (int i = 0; i < 30; i++) {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 0, 0, 0, 0, default(Color), 1.2f);
                }
            }
            
            projectile.ai[0] += 1f;

            if ((int)projectile.ai[0] == EarthDuration) {
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft) {
            Main.PlaySound(SoundID.Tink, projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 0, 0, 0, 0, default(Color), 1.2f);
            }
        }
        
        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(EarthDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            EarthDuration = reader.ReadInt32();
        }
    }
}