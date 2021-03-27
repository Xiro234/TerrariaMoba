using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class WBBottle : ModProjectile {

        public int PoolDamage { get; set; }
        public int PoolDuration { get; set; }

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
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == projectile.owner) { 
                Projectile proj = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, 
                    TerrariaMoba.Instance.ProjectileType("WBWhirlpool"), PoolDamage, 0, projectile.whoAmI);
                Main.PlaySound(SoundID.Item27, projectile.position);
                
                WBWhirlpool pool = proj.modProjectile as WBWhirlpool;
                if (pool != null) {
                    pool.PoolDuration = PoolDuration;
                }
            }
        }
    }
}