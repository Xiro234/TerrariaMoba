using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESLightning : ModProjectile {
        public override void SetDefaults() {
            Projectile.Name = "Lightning Bolt";
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 4;
        }

        public override void AI() {
            for (int i = 0; i < 4; i++) {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = Main.rand.Next(60, 100) * 0.013f;
                Main.dust[dust].velocity *= 0.2f;
            }
        }
    }
}