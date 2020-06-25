using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Players;

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
            //Character mychar = Main.player[projectile.owner].GetModPlayer<MobaPlayer>().MyCharacter;
            projectile.ai[0] += 1f;

            if (((int)projectile.ai[0] % 40) == 0) {
                Vector2 newPos = new Vector2(projectile.position.X, GetYPos());
                Projectile.NewProjectile(newPos, Vector2.Zero, mod.ProjectileType("Earthsplitter"), projectile.damage, projectile.knockBack, projectile.owner);
            }
            if (projectile.ai[0] >= 200) {
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