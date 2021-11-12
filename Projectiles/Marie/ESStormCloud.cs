using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Marie;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESStormCloud : ModProjectile {
        
        public int LightningDamage { get; set; }
        public float LightningSpeed { get; set; }
        public int RainDamage { get; set; }
        public float RainSpeed { get; set; }
        public int CloudDuration { get; set; }
        
        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults() {
            Projectile.Name = "ES Storm Cloud";
            Projectile.width = 366; 
            Projectile.height = 104; 
            Projectile.timeLeft = 1000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            
            LightningDamage = EyeOfTheStorm.LIGHTNING_DAMAGE;
            LightningSpeed = EyeOfTheStorm.LIGHTNING_SPEED;
            RainDamage = EyeOfTheStorm.RAIN_DAMAGE;
            RainSpeed = EyeOfTheStorm.RAIN_SPEED;
            CloudDuration = EyeOfTheStorm.CLOUD_DURATION;
        }

        public override void AI() {
            if (Projectile.timeLeft == 1000) {
                Projectile.timeLeft = CloudDuration;
            }

            if (Projectile.timeLeft > 20) {
                Projectile.ai[0] += 1f;
                Projectile.ai[1] += 1f;
            }

            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
            
            if (Projectile.ai[0] >= 4f) {
                Projectile.ai[0] = 0f;
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                    int rainX = (int) (Projectile.position.X + 14f + Main.rand.Next(Projectile.width - 18));
                    int rainY = (int) Projectile.position.Y + Projectile.height;
                    Vector2 pos = new Vector2(rainX, rainY);
                    Vector2 vel = new Vector2(0f, RainSpeed);
                    Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), pos, vel, ModContent.ProjectileType<ESRain>(), 
                        RainDamage, 0f, Main.myPlayer);
                }
            }

            if (Projectile.ai[1] >= 45f) {
                Projectile.ai[1] = 0f;
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                    int lightX = (int) (Projectile.position.X + 14f + Main.rand.Next(Projectile.width - 18));
                    int lightY = (int) (Projectile.position.Y + Projectile.height - 20f);
                    Vector2 pos = new Vector2(lightX, lightY);
                    Vector2 vel = new Vector2(0f, LightningSpeed);
                    Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), pos, vel, ModContent.ProjectileType<ESLightning>(), 
                        LightningDamage, 0f, Main.myPlayer);
                }
            }
        }
        
        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(LightningDamage);
            writer.Write(LightningSpeed);
            writer.Write(RainDamage);
            writer.Write(RainSpeed);
            writer.Write(CloudDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            LightningDamage = reader.ReadInt32();
            LightningSpeed = reader.ReadSingle();
            RainDamage = reader.ReadInt32();
            RainSpeed = reader.ReadSingle();
            CloudDuration = reader.ReadInt32();
        }
    }
}