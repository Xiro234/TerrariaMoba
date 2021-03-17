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
            projectile.friendly = true;
            projectile.width = 0;
            projectile.height = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;

            TrapDamage = EnsnaringVinesAbility.TRAP_BASE_DAMAGE;
            TrapDuration = EnsnaringVinesAbility.TRAP_BASE_DURATION;
            NumberOfTraps = EnsnaringVinesAbility.TRAP_BASE_NUMBER;
            TileDistance = EnsnaringVinesAbility.TRAP_BASE_TILE_DISTANCE;
        }

        public override bool CanDamage() {
            return false;
        }

        public override void AI() {
            int timeBetween = (int) ((TileDistance * 16) / projectile.velocity.Length());
            if (((int)projectile.ai[0] % timeBetween) == 0){
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == projectile.owner) {
                    Vector2 newPos = new Vector2(projectile.position.X, GetYPos());
                    Projectile proj = Projectile.NewProjectileDirect(newPos, Vector2.Zero, TerrariaMoba.Instance.ProjectileType("EnsnaringVinesTrap"),
                        TrapDamage, 0, projectile.whoAmI);
                    
                    EnsnaringVinesTrap trap = proj.modProjectile as EnsnaringVinesTrap;

                    if (trap != null) {
                        trap.TrapDuration = TrapDuration;
                    }
                }
            }

            projectile.ai[0] += 1f;

            if ((int)projectile.ai[0] == (NumberOfTraps * timeBetween)) {
                projectile.Kill();
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
            int posX = (int)projectile.Bottom.X;
            int posY = (int)projectile.Bottom.Y;

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