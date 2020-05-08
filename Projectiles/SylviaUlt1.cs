using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Utils;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles {
    public class SylviaUlt1 : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt1");
        }
        
        public override void SetDefaults() {
            projectile.height = 48;
            projectile.width = 30;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.tileCollide = true;
        }

        public override void AI() {
            if (projectile.ai[0] == 0) {
                Main.PlaySound(6, projectile.position);
                for (int i = 0; i < 20; i++) {
                    Dust.NewDust(Main.player[projectile.owner].position, projectile.width, projectile.height, 57, 0,
                        0, 150, Color.LightGreen, 0.7f);
                }
            }

            projectile.ai[0] += 1;

            if (projectile.ai[0] == 15) {
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft) {
            Player player = Main.player[projectile.owner];
            
            //if(Main.netMode != NetmodeID.MultiplayerClient) {
                player.Teleport(projectile.position, -1);
           // }
           // if (Main.netMode == NetmodeID.Server) {
              //  NetMessage.SendData(65, -1, -1, null, 0, (float)player.whoAmI, projectile.position.X, projectile.position.Y, -1, 0, 0);
           // }
            
            Main.PlaySound(6, projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }

            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().IsPhasing = false;
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().SylviaUlt1 = true;
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().NumberJavelins = 3;
        }
    }
}