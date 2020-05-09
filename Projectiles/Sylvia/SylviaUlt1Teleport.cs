using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Stats;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt1Teleport : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt1Teleport");
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
                Player player = Main.player[projectile.owner];
                player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().IsPhasing = true;
                Main.PlaySound(6, projectile.position);
                for (int i = 0; i < 20; i++) {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
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
            
            player.Teleport(projectile.position, -1);

            Main.PlaySound(6, projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
            
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().IsPhasing = false;
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().SylviaUlt1 = true;
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().NumberJavelins = 3;
            player.GetModPlayer<TerrariaMobaPlayer_Gameplay>().SylviaUlt1Timer = 300;
        }

        public override bool? CanCutTiles() {
            return false;
        }
    }
}