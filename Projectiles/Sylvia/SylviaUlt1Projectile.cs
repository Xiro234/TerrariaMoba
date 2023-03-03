using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using System;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt1Projectile : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Chlorophyte Javelin");
        }
        
        public override void SetDefaults() {
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;  
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            for (int i = 0; i < 12; i++) {
                Dust.NewDust(Projectile.position + new Vector2(1, 0), Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0, 0, 120, Color.LightGreen, 0.8f);
            }
        }
        
        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GrassBlades, 0, 0, 0, default, 1f);
            }
        }
    }
}