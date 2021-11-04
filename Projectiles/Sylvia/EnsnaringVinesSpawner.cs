using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Sylvia;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class EnsnaringVinesSpawner : ModProjectile {
        
        public int TrapDamage { get; set; }
        public int TrapDuration { get; set; }
        public int NumberOfTraps { get; set; }
        public int TileDistance { get; set; }
        
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("EnsnaringVines");
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.width = 0;
            Projectile.height = 0;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;

            TrapDamage = EnsnaringVines.TRAP_DAMAGE;
            TrapDuration = EnsnaringVines.TRAP_DURATION;
            NumberOfTraps = EnsnaringVines.TRAP_AMOUNT;
            TileDistance = EnsnaringVines.TRAP_DISTANCE;
        }

        public override bool? CanDamage() {
            return false;
        }

        public override void AI() {
            int timeBetween = (int) ((TileDistance * 16) / Projectile.velocity.Length());
            if (((int)Projectile.ai[0] % timeBetween) == 0){
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                    Vector2 newPos = new Vector2(Projectile.position.X, GetYPos());
                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetProjectileSource_FromThis(), newPos, Vector2.Zero, ModContent.ProjectileType<EnsnaringVinesTrap>(),
                        TrapDamage, 0, Projectile.whoAmI);
                    
                    EnsnaringVinesTrap trap = proj.ModProjectile as EnsnaringVinesTrap;

                    if (trap != null) {
                        trap.TrapDuration = TrapDuration;
                    }
                }
            }

            Projectile.ai[0] += 1f;

            if ((int)Projectile.ai[0] == (NumberOfTraps * timeBetween)) {
                Projectile.Kill();
            }
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(TrapDamage);
            writer.Write(TrapDuration);
            writer.Write(NumberOfTraps);
            writer.Write(TileDistance);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            TrapDamage = reader.ReadInt32();
            TrapDuration = reader.ReadInt32();
            NumberOfTraps = reader.ReadInt32();
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