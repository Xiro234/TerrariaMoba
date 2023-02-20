using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class FungalSpore : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Fungal Spore");
        }

        public override void SetDefaults() {
            Projectile.width = 20;
            Projectile.height = 32;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch, 0f, 0f, 100, Color.LightSeaGreen, 1.3f);
            Main.dust[dust].noGravity = true;
        }
    }
}