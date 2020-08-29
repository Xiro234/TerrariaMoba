using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles.Osteo {
    public class OsteoSoul : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("OsteoSoul");
        }

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 20;
            projectile.height = 32;
            projectile.aiStyle = 0;
        }

        public override void Kill(int timeLeft) {
            
        }
    }
}