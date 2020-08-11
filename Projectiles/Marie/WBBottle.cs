using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class WBBottle : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.penetrate = 1;
        }

        public override void AI() {
            projectile.ai[0] += 1f;
            if(projectile.ai[0] >= 10f) {
                projectile.velocity.Y += 0.25f;
                projectile.velocity.X *= 0.99f;
            }
            
            projectile.rotation += projectile.velocity.X * 0.1f;
            
            for (int i = 0; i < 6; i++) {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 137);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.013f;
                Main.dust[dust].velocity *= 0.2f;
            }
        }

        public override void Kill(int timeLeft) {
            Player player = Main.player[projectile.owner];
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, 
                    TerrariaMoba.Instance.ProjectileType("WBWhirlpool"), 70, 0, player.whoAmI);
                Main.PlaySound(SoundID.Item27, projectile.position);
            }
        }
    }
}