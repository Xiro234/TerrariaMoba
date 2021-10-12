using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace TerrariaMoba.Projectiles.Nocturne {
    public class NocturneDash : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("NocturneDash");
        }
        
        public override void SetDefaults() {
            Projectile.height = 58;
            Projectile.width = 32;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.tileCollide = true;
            Projectile.alpha = 128;
        }

        public override void AI() {
            if(Projectile.alpha > 0) {
                Projectile.alpha -= 2;
            } else {
                Projectile.alpha = 0;
            }

            for (int i = 0; i < 5; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 8, 0,
                    0, 100);
            }
        }
        
        public override bool PreDraw(ref Color lightColor) {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                if (Main.player[Projectile.owner].direction == 1) {
                    Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.Tan,
                        Projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), Projectile.scale, 
                        SpriteEffects.FlipHorizontally, 0);
                } else {
                    Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.Tan,
                        Projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), Projectile.scale, 
                        0, 0);
                }
            }
            return false;
        }
        
        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(0, Projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 8);
            }
        }
    }
}