using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Projectiles.Marie {
    public class FountainOfLacusia : ModProjectile {
        public override void SetStaticDefaults() {
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults() {
            projectile.Name = "Fountain of Lacusia";
            projectile.width = 32;
            projectile.height = 64;
            //projectile.scale = 2f;
            projectile.timeLeft = 360;
        }

        public override void AI() {
            projectile.ai[0] += 1f;
            if ((int)projectile.ai[0] % 30 == 0) {
                Vector2 fountainPos = projectile.Center;
                float radius = 20 * 16.0f;
                Main.PlaySound(SoundID.Item4, projectile.Center);

                for (int a = 0; a < 360; a++) {
                    int xPos = (int)(fountainPos.X + radius * Math.Cos(TerrariaMobaUtils.Conv2Rad(a)));
                    int yPos = (int)(fountainPos.Y + radius * Math.Sin(TerrariaMobaUtils.Conv2Rad(a)));
                    int dust = Dust.NewDust(new Vector2(xPos, yPos), 2, 2, 113, 0, 0, 0, default(Color));
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                }
            }

            if (++projectile.frameCounter >= 5) {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }

            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active && plr != null) {
                    if (plr.team == Main.player[projectile.owner].team) {
                        float dist = (plr.Center - projectile.Center).Length() / 16.0f;
                        if (dist <= 20) {
                            plr.AddBuff(BuffType<Buffs.BlessingOfLacusia>(), 10);
                        }
                    }
                }
            }
        }
    }
}