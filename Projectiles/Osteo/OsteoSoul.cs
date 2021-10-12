using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class OsteoSoul : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("OsteoSoul");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 20;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
        }

        public override void Kill(int timeLeft) {
            
        }
    }
}