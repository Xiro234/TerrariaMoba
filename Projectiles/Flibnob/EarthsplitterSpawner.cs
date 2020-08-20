using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Flibnob {
    public class EarthsplitterSpawner : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Earthsplitter");
        }

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 0;
            projectile.height = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;
        }

        public override void AI() {
            Player player = Main.player[projectile.owner];
            projectile.ai[0] += 1f;

            if ((int)projectile.ai[0] % 15 == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                    Vector2 newPos = new Vector2(projectile.position.X, GetYPos()); 
                    Projectile.NewProjectile(newPos.X, newPos.Y - 8f, 0f, 0f, mod.ProjectileType("Earthsplitter"), projectile.damage, projectile.knockBack, player.whoAmI);
                }
            }
            if (projectile.ai[0] >= 90) {
                projectile.Kill();
            }
        }

        public override bool CanDamage() {
            return false;
        }

        public int GetYPos() {
            int posX = (int)projectile.Bottom.X;
            int posY = (int)projectile.Bottom.Y;

            if (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                while (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY -= 1;
                }
            } else {
                while (!TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY += 1;
                }
            }
            return posY;
        }
    }
}