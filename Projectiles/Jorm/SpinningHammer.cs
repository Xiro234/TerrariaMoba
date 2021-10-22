using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Jorm;

namespace TerrariaMoba.Projectiles.Jorm {
    public class SpinningHammer : ModProjectile {

        public float SpinRadius { get; set; }
        public float SpawnSpeed { get; set; }
        public bool IsOrbiting;

        public override void SetDefaults() {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.timeLeft = 360;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            SpinRadius = DanceOfTheGoldenhammer.HAMMER_SPIN_RADIUS;
            SpawnSpeed = DanceOfTheGoldenhammer.HAMMER_SPAWN_SPEED;
        }

        public override void AI() {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += 4 * 0.05f;
            Projectile.ai[1] += SpawnSpeed;
            
            if((Projectile.Center - player.Center).Length() >= SpinRadius && !IsOrbiting) {
                IsOrbiting = true;
                    
                Vector2 offset = Projectile.Center - player.Center;
                float angleToPlayer = (float)(Math.Atan2(offset.Y, offset.X) * 180.0 / Math.PI);
                Projectile.ai[0] = angleToPlayer < 0 ? angleToPlayer + 450f : angleToPlayer + 90f;
            }

            if (IsOrbiting) {
                Projectile.Center = player.Center + new Vector2(0f, -SpinRadius).RotatedBy(MathHelper.ToRadians(Projectile.ai[0]));
                Projectile.ai[0] += 1f;
            } else {
                Projectile.Center = player.Center + Projectile.velocity * Projectile.ai[1];
            }

            for (int d = 0; d < 2; d++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 269, 0, 0, 200);
            }
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(SpinRadius);
            writer.Write(SpawnSpeed);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            SpinRadius = reader.ReadSingle();
            SpawnSpeed = reader.ReadSingle();
        }
    }
}