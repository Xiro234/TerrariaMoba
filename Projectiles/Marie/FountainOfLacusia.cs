using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class FountainOfLacusia : ModProjectile {
        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults() {
            Projectile.Name = "Fountain of Lacusia";
            Projectile.width = 32;
            Projectile.height = 64;
            //Projectile.scale = 2f;
            Projectile.timeLeft = 360;
        }

        public override void AI() {
            Projectile.ai[0] += 1f;
            if ((int)Projectile.ai[0] % 30 == 0) {
                Vector2 fountainPos = Projectile.Center;
                float radius = 20 * 16.0f;
                SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);

                for (int a = 0; a < 360; a++) {
                    int xPos = (int)(fountainPos.X + radius * Math.Cos(TerrariaMobaUtils.Conv2Rad(a)));
                    int yPos = (int)(fountainPos.Y + radius * Math.Sin(TerrariaMobaUtils.Conv2Rad(a)));
                    int dust = Dust.NewDust(new Vector2(xPos, yPos), 2, 2, DustID.Clentaminator_Blue, 0, 0, 0, default(Color));
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                }
            }

            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }

            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active && plr != null) {
                    if (plr.team == Main.player[Projectile.owner].team) {
                        float dist = (plr.Center - Projectile.Center).Length() / 16.0f;
                        if (dist <= 20) {
                        }
                    }
                }
            }
        }
    }
}