using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Stats;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class EnsnaringVinesSpawner : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("EnsnaringVines");
        }

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 0;
            projectile.height = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;
        }

        public override void AI() {
            projectile.ai[0] += 1f;
            
            if (((int)projectile.ai[0] & 15) == 0) {
                Vector2 newPos = new Vector2(projectile.position.X, GetYPos());
                Projectile.NewProjectile(newPos, Vector2.Zero, mod.ProjectileType("EnsnaringVines"), projectile.damage, projectile.knockBack, projectile.owner);
            }
            
            //var player = Main.player[projectile.owner].GetModPlayer<TerrariaMobaPlayer_Gameplay>();

            /*
            if (player.MyCharacter.talentArray[2, 2]) {
                if (projectile.ai[0] == 135) {
                    projectile.Kill();
                }
            }
            else {
            */
                if (projectile.ai[0] == 90) {
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
            }
            else {
                while (!TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY += 1;
                }
            }
            return posY;
        }
    }
}