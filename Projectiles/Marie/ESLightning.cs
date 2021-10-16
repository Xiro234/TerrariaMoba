using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESLightning : ModProjectile {
        public override void SetDefaults() {
            Projectile.Name = "Maelstrom Beam";
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 4;
            Projectile.timeLeft = 600;
        }

        public override void AI() {
            for (int i = 0; i < 4; i++) {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(60, 100) * 0.013f;
                Main.dust[dust].velocity *= 0.2f;
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            target.AddBuff(BuffID.Electrified, 75, false);
        }
    }
}