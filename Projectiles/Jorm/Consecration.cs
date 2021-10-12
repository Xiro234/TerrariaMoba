﻿using System.IO;
 using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Jorm {
    public class Consecration : ModProjectile {
        public float ConsecSpread { get; set; }
        public float ConsecDuration { get; set; }

        public override void SetDefaults() {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 1000;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = -1;

            ConsecSpread = Abilities.Jorm.Consecration.CONSEC_SPREAD_RANGE;
            ConsecDuration = Abilities.Jorm.Consecration.CONSEC_DURATION;
        }

        public override void AI() {
            if (projectile.timeLeft == 1000) {
                projectile.timeLeft = (int) ConsecDuration;
            }

            projectile.position.Y = GetYPos() - 2;
            
            if (projectile.width < ConsecSpread) {
                projectile.width += 5;
                projectile.position.X -= 2.5f;
            }

            for (int d = 0; d < 20; d++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 269, 0, 0, 50);
            }

        }
        
        private int GetYPos() {
            int posX = (int)projectile.Bottom.X;
            int posY = (int)projectile.Bottom.Y;

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
        
        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(ConsecSpread);
            writer.Write(ConsecDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            ConsecSpread = reader.ReadSingle();
            ConsecDuration = reader.ReadSingle();
        }
    }
}