using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Jorm;

namespace TerrariaMoba.Projectiles.Jorm {
    public class HammerfallProjSpawner : ModProjectile {
        
        public float HammerDamage { get; set; }
        public float HammerSpeed { get; set; }
        public int NumberOfHammers { get; set; }
        public int TileDistance { get; set; }

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Hammerfall");
        }
        
        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 0;
            Projectile.height = 0;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;

            HammerDamage = Hammerfall.BIGHAMMER_DAMAGE;
            HammerSpeed = Hammerfall.BIGHAMMER_SPEED;
            NumberOfHammers = Hammerfall.BIGHAMMER_NUMBER;
            TileDistance = Hammerfall.BIGHAMMER_TILE_DISTANCE;
        }
        
        public override bool? CanDamage() {
            return false;
        }
        
        public override void AI() {
            int timeBetween = (int) ((TileDistance * 16) / Projectile.velocity.Length());
            if (((int)Projectile.ai[0] % timeBetween) == 0){
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                    Vector2 velocity = new Vector2(0, HammerSpeed);
                    Projectile.NewProjectileDirect(Projectile.GetProjectileSource_FromThis(), Projectile.position,
                        velocity, ModContent.ProjectileType<HammerfallProj>(),
                        (int)HammerDamage, 0, Main.myPlayer, HammerSpeed);
                }
            }

            Projectile.ai[0] += 1f;

            if ((int)Projectile.ai[0] == (NumberOfHammers * timeBetween)) {
                Projectile.Kill();
            }
        }
        
        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(HammerDamage);
            writer.Write(HammerSpeed);
            writer.Write(NumberOfHammers);
            writer.Write(TileDistance);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            HammerDamage = reader.ReadSingle();
            HammerSpeed = reader.ReadSingle();
            NumberOfHammers = reader.ReadInt32();
            TileDistance = reader.ReadInt32();
        }

        private int GetYPos() {
            int posX = (int)Projectile.Bottom.X;
            int posY = (int)Projectile.Bottom.Y;

            if (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                while (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY -= 1;
                }
            }
            else {
                while (!TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY += 1;
                }
            }
            return posY;
        }
        
    }
}