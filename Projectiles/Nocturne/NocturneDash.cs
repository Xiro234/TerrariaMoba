using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace TerrariaMoba.Projectiles.Nocturne {
    public class NocturneDash : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("NocturneDash");
        }
        
        public override void SetDefaults() {
            projectile.height = 58;
            projectile.width = 32;
            projectile.friendly = true;
            projectile.aiStyle = 0;
            projectile.tileCollide = true;
            projectile.alpha = 128;
        }

        public override void AI() {
            if(projectile.alpha > 0) {
                projectile.alpha -= 2;
            } else {
                projectile.alpha = 0;
            }

            for (int i = 0; i < 5; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 8, 0,
                    0, 100);
            }
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            Texture2D texture = Main.projectileTexture[projectile.type];
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == projectile.owner) {
                if (Main.player[projectile.owner].direction == 1) {
                    spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.Tan,
                        projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), projectile.scale, 
                        SpriteEffects.FlipHorizontally, 0);
                } else {
                    spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.Tan,
                        projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), projectile.scale, 
                        0, 0);
                }
            }
            return false;
        }
        
        public override void Kill(int timeLeft) {
            Main.PlaySound(0, projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 8);
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
    }
}