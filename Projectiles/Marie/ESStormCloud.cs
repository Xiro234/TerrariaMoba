using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESStormCloud : ModProjectile {
        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults() {
            Projectile.Name = "ES Storm Cloud";
            Projectile.width = 366; 
            Projectile.height = 104; 
            Projectile.timeLeft = 380;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }

        public override void AI() {
            if (Projectile.timeLeft > 20) {
                Projectile.ai[0] += 1f;
                Projectile.ai[1] += 1f;
            }

            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
            
            if (Projectile.ai[0] >= 4f) {
                Projectile.ai[0] = 0f;
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                    int rainX = (int) (Projectile.position.X + 14f + Main.rand.Next(Projectile.width - 18));
                    int rainY = (int) Projectile.position.Y + Projectile.height;
                    Vector2 pos = new Vector2(rainX, rainY);
                    Vector2 vel = new Vector2(0f, 4.25f);
                    Projectile.NewProjectile(pos, vel, ModContent.ProjectileType<ESRain"), 
                        0, 0f, Projectile.whoAmI);
                }
            }

            if (Projectile.ai[1] >= 45f) {
                Projectile.ai[1] = 0f;
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                    int lightX = (int) (Projectile.position.X + 14f + Main.rand.Next(Projectile.width - 18));
                    int lightY = (int) (Projectile.position.Y + Projectile.height - 20f);
                    Vector2 pos = new Vector2(lightX, lightY);
                    Vector2 vel = new Vector2(0f, 3.5f);
                    Projectile.NewProjectile(pos, vel, ModContent.ProjectileType<ESLightning"), 
                        0, 0f, Projectile.whoAmI);
                }
            }
        }
    }
}