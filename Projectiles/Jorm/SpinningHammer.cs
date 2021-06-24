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
            projectile.width = 38;
            projectile.height = 38;
            projectile.timeLeft = 360;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            SpinRadius = DanceOfTheGoldenhammer.HAMMER_SPIN_RADIUS;
            SpawnSpeed = DanceOfTheGoldenhammer.HAMMER_SPAWN_SPEED;
        }

        public override void AI() {
            Player player = Main.player[projectile.owner];
            projectile.rotation += 4 * 0.05f;
            projectile.ai[1] += SpawnSpeed;
            
            if((projectile.Center - player.Center).Length() >= SpinRadius && !IsOrbiting) {
                IsOrbiting = true;
                    
                Vector2 offset = projectile.Center - player.Center;
                float angleToPlayer = (float)(Math.Atan2(offset.Y, offset.X) * 180.0 / Math.PI);
                projectile.ai[0] = angleToPlayer < 0 ? angleToPlayer + 450f : angleToPlayer + 90f;
            }

            if (IsOrbiting) {
                projectile.Center = player.Center + new Vector2(0f, -SpinRadius).RotatedBy(MathHelper.ToRadians(projectile.ai[0]));
                projectile.ai[0] += 1f;
            }
            else {
                projectile.Center = player.Center + projectile.velocity * projectile.ai[1];
            }

            for (int d = 0; d < 2; d++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 269, 0, 0, 200);
            }
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(SpinRadius);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            SpinRadius = reader.ReadSingle();
        }
    }
}