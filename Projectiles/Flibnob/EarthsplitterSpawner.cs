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
            projectile.friendly = true;
            projectile.width = 0;
            projectile.height = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;

            EarthDamage = Earthsplitter.EARTH_BASE_DAMAGE;
            EarthDuration = Earthsplitter.EARTH_BASE_DURATION;
            NumberOfEarths = Earthsplitter.EARTH_BASE_NUMBER;
            EarthDistance = Earthsplitter.EARTH_BASE_DELAY;
        }

        public override void AI() {
            int timeBetween = (int) ((EarthDistance * 16) / projectile.velocity.Length());
            if (((int)projectile.ai[0] % timeBetween) == 0){
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == projectile.owner) {
                    Vector2 newPos = new Vector2(projectile.position.X, GetYPos());
                    newPos.Y -= 8f;
                    Projectile proj = Projectile.NewProjectileDirect(newPos, Vector2.Zero, TerrariaMoba.Instance.ProjectileType("SplitEarth"),
                        EarthDamage, 0, projectile.whoAmI);
                    
                    SplitEarth earth = proj.modProjectile as SplitEarth;

                    if (earth != null) {
                        earth.EarthDuration = EarthDuration;
                    }
                }
            }

            projectile.ai[0] += 1f;

            if ((int)projectile.ai[0] == (NumberOfEarths * timeBetween)) {
                projectile.Kill();
            }
        }

        public override bool CanDamage() {
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
            int posX = (int)projectile.Bottom.X;
            int posY = (int)projectile.Bottom.Y;

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