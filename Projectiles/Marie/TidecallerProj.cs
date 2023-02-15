using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Marie;

namespace TerrariaMoba.Projectiles.Marie {
    public class TidecallerProj : ModProjectile {
        public int TideDamage { get; set; }
        public int TideDuration { get; set; }

        public override void SetDefaults() {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            TideDamage = Tidecaller.TIDE_DAMAGE;
            TideDuration = Tidecaller.TIDE_DURATION;
        }

        public override bool? CanDamage() {
            return false;
        }

        public override void AI() {
            int x = (int)Projectile.Bottom.X;
            int y = (int)Projectile.Bottom.Y;
            if (TerrariaMobaUtils.TileIsSolidOrPlatform(x / 16, y / 16)) {
                Projectile.position.X -= 60;
                Projectile.position.Y -= 25;
                Projectile.Kill();
            }

            Projectile.ai[0] += 1f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (Projectile.ai[0] < 15) {
                Projectile.alpha = (int)((255 / 15) * (15 - Projectile.ai[0]));
            } else {
                Projectile.alpha = 0;
            }

            Vector2 direction = Vector2.Normalize(Projectile.velocity);

            for (int i = 0; i < 3; i++) {
                float val = (float)Math.Sin(6 * MathHelper.ToRadians(Projectile.ai[0]) + (i / 2));
                Vector2 position1 = Projectile.Center + (new Vector2(-direction.Y, direction.X) * (val * 15));
                Vector2 position2 = Projectile.Center + (new Vector2(direction.Y, -direction.X) * (val * 15));

                Dust.NewDustPerfect(position1, DustID.WhiteTorch, Vector2.Zero, Projectile.alpha, Color.CornflowerBlue, 1.4f);
                Dust.NewDustPerfect(position2, DustID.WhiteTorch, Vector2.Zero, Projectile.alpha, Color.CornflowerBlue, 1.4f);
            }
        }

        public override void Kill(int timeLeft) {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner) {
                float x = Projectile.TopLeft.X;
                float y = Projectile.TopLeft.Y;
                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), new Vector2(x + 81f, y + 15f), Vector2.Zero,
                    ModContent.ProjectileType<TidecallerTide>(), 1, 0f, Main.myPlayer);
                TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: TideDamage);

                TidecallerTide tide = proj.ModProjectile as TidecallerTide;
                if (tide != null) {
                    tide.TideDuration = TideDuration;
                }
            }
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(TideDamage);
            writer.Write(TideDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            TideDamage = reader.ReadInt32();
            TideDuration = reader.ReadInt32();
        }
    }
}
