using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using TerrariaMoba.Abilities.Osteo;

namespace TerrariaMoba.Projectiles.Osteo {
    public class FungalSpore : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Fungal Spore");
        }

        public int SporeLifetime { get; set; }

        public override void SetDefaults() {
            Projectile.width = 20;
            Projectile.height = 32;
            Projectile.timeLeft = 1000;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            SporeLifetime = FungalArmor.SPORE_LIFEITME;
        }

        public override void AI() {
            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = SporeLifetime - 1;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch, 0f, 0f, 100, Color.LightSeaGreen, 1.3f);
            Main.dust[dust].noGravity = true;
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(SporeLifetime);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            SporeLifetime = reader.ReadInt32();
        }
    }
}