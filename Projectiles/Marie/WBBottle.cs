using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Marie;

namespace TerrariaMoba.Projectiles.Marie {
    public class WBBottle : ModProjectile {

        public int PoolDamage { get; set; }
        public int PoolDuration { get; set; }

        public override void SetDefaults() {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = 1;

            PoolDamage = Tidecaller.POOL_DAMAGE;
            PoolDuration = Tidecaller.POOL_DURATION;
        }

        public override void AI() {
            Projectile.ai[0] += 1f;
            if(Projectile.ai[0] >= 10f) {
                Projectile.velocity.Y += 0.25f;
                Projectile.velocity.X *= 0.99f;
            }
            
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            
            for (int i = 0; i < 6; i++) {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IcyMerman);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.013f;
                Main.dust[dust].velocity *= 0.2f;
            }
        }

        public override void Kill(int timeLeft) {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) { 
                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, 
                    ModContent.ProjectileType<WBWhirlpool>(), 1, 0, Main.myPlayer);
                TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: PoolDamage);
                
                SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
                
                WBWhirlpool pool = proj.ModProjectile as WBWhirlpool;
                if (pool != null) {
                    pool.PoolDuration = PoolDuration;
                }
            }
        }
        
        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(PoolDamage);
            writer.Write(PoolDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            PoolDamage = reader.ReadInt32();
            PoolDuration = reader.ReadInt32();
        }
    }
}