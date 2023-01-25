using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Marie;

namespace TerrariaMoba.Projectiles.Marie {
    public class ESSpawner : ModProjectile {

        public int LightningDamage { get; set; }
        public float LightningSpeed { get; set; }
        public int RainDamage { get; set; }
        public float RainSpeed { get; set; }
        public int CloudDamage { get; set; }
        public int CloudDuration { get; set; }

        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults() {
            Projectile.Name = "Storm Eye";
            Projectile.netImportant = true;
            Projectile.width = 28; 
            Projectile.height = 28; 
            Projectile.timeLeft = 100;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            
            LightningDamage = EyeOfTheStorm.LIGHTNING_DAMAGE;
            LightningSpeed = EyeOfTheStorm.LIGHTNING_SPEED;
            RainDamage = EyeOfTheStorm.RAIN_DAMAGE;
            RainSpeed = EyeOfTheStorm.RAIN_SPEED;
            CloudDamage = EyeOfTheStorm.CLOUD_DAMAGE;
            CloudDuration = EyeOfTheStorm.CLOUD_DURATION;
        }

        public override void AI() {
            if (Projectile.alpha > 0) {
                Projectile.alpha -= 8;
            }

            if (++Projectile.frameCounter >= 5) {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
        }

        public override void Kill(int timeLeft) {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, 
                    ModContent.ProjectileType<ESStormCloud>(), 1, 0f, Main.myPlayer);
                TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: CloudDamage);
                
                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);

                ESStormCloud cloud = proj.ModProjectile as ESStormCloud;

                if (cloud != null) {
                    cloud.LightningDamage = LightningDamage;
                    cloud.LightningSpeed = LightningSpeed;
                    cloud.RainDamage = RainDamage;
                    cloud.RainSpeed = RainSpeed;
                    cloud.CloudDuration = CloudDuration;
                }
            }  
        }
        
        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(LightningDamage);
            writer.Write(LightningSpeed);
            writer.Write(RainDamage);
            writer.Write(RainSpeed);
            writer.Write(CloudDamage);
            writer.Write(CloudDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            LightningDamage = reader.ReadInt32();
            LightningSpeed = reader.ReadSingle();
            RainDamage = reader.ReadInt32();
            RainSpeed = reader.ReadSingle();
            CloudDamage = reader.ReadInt32();
            CloudDuration = reader.ReadInt32();
        }
    }
}