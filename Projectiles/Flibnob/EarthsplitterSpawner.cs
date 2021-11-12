using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Flibnob;

namespace TerrariaMoba.Projectiles.Flibnob {
    public class EarthsplitterSpawner : ModProjectile {
        
        public int EarthDamage { get; set; }
        public int EarthDuration { get; set; }
        public int NumberOfEarths { get; set; }
        public int EarthDistance { get; set; }
        
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Earthsplitter");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 0;
            Projectile.height = 0;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;

            EarthDamage = Earthsplitter.EARTH_BASE_DAMAGE;
            EarthDuration = Earthsplitter.EARTH_BASE_DURATION;
            NumberOfEarths = Earthsplitter.EARTH_BASE_NUMBER;
            EarthDistance = Earthsplitter.EARTH_BASE_DELAY;
        }

        public override void AI() {
            int timeBetween = (int) ((EarthDistance * 16) / Projectile.velocity.Length());
            if (((int)Projectile.ai[0] % timeBetween) == 0){
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                    Vector2 newPos = new Vector2(Projectile.position.X, GetYPos());
                    newPos.Y -= 8f;
                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetProjectileSource_FromThis(), newPos, Vector2.Zero, ModContent.ProjectileType<SplitEarth>(),
                        EarthDamage, 0, Main.myPlayer);
                    
                    SplitEarth earth = proj.ModProjectile as SplitEarth;

                    if (earth != null) {
                        earth.EarthDuration = EarthDuration;
                    }
                }
            }

            Projectile.ai[0] += 1f;

            if ((int)Projectile.ai[0] == (NumberOfEarths * timeBetween)) {
                Projectile.Kill();
            }
        }

        public override bool? CanDamage() {
            return false;
        }
        
        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(EarthDamage);
            writer.Write(EarthDuration);
            writer.Write(NumberOfEarths);
            writer.Write(EarthDistance);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            EarthDamage = reader.ReadInt32();
            EarthDuration = reader.ReadInt32();
            NumberOfEarths = reader.ReadInt32();
            EarthDistance = reader.ReadInt32();
        }

        public int GetYPos() {
            int posX = (int)Projectile.Bottom.X;
            int posY = (int)Projectile.Bottom.Y;

            if (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                while (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY -= 1;
                }
            } else {
                while (!TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY += 1;
                }
            }
            return posY;
        }
    }
}