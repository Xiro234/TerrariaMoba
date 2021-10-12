using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class WBBottle : ModProjectile {

        public int PoolDamage { get; set; }
        public int PoolDuration { get; set; }

        public override void SetDefaults() {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
        }

        public override void AI() {
            Projectile.ai[0] += 1f;
            if(Projectile.ai[0] >= 10f) {
                Projectile.velocity.Y += 0.25f;
                Projectile.velocity.X *= 0.99f;
            }
            
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            
            for (int i = 0; i < 6; i++) {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 137);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.013f;
                Main.dust[dust].velocity *= 0.2f;
            }
        }

        public override void Kill(int timeLeft) {
            Player player = Main.player[Projectile.owner];
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) { 
                Projectile proj = Projectile.NewProjectileDirect(Projectile.Center, Vector2.Zero, 
                    ModContent.ProjectileType<WBWhirlpool"), PoolDamage, 0, Projectile.whoAmI);
                SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
                
                WBWhirlpool pool = proj.ModProjectile as WBWhirlpool;
                if (pool != null) {
                    pool.PoolDuration = PoolDuration;
                }
            }
        }
    }
}