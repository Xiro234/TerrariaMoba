using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrariaMoba.Abilities.Sylvia;
using System.IO;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class SylviaUlt2 : ModProjectile {
        
        public int SporeDamage { get; set; }
        public int NumberOfSpores { get; set; }
        public int SporeDuration { get; set; }
        
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SylviaUlt2");
        }
        
        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 0;
            DrawOffsetX = -46;

            SporeDamage = PlanterasLastWill.SPORE_DAMAGE;
            NumberOfSpores = PlanterasLastWill.SPORE_COUNT;
            SporeDuration = PlanterasLastWill.SPORE_DURATION;
        }

        public override void AI() {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (Projectile.ai[0] < 15) {
                Projectile.alpha = (int) ((255 / 15) * (15 - Projectile.ai[0]));
            }
            else {
                Projectile.alpha = 0;
            }

            Vector2 direction = Vector2.Normalize(Projectile.velocity);

            for (int i = 0; i < 3; i++) {
                float val = (float) Math.Sin(6 * MathHelper.ToRadians(Projectile.ai[0]) + (i / 2));
                Vector2 position1 = Projectile.Center + (new Vector2(-direction.Y, direction.X) * (val * 30));
                Vector2 position2 = Projectile.Center + (new Vector2(direction.Y, -direction.X) * (val * 30));

                Dust.NewDustPerfect(position1 - (direction * 100), 131, Vector2.Zero, Projectile.alpha, Color.ForestGreen, 1f);
                Dust.NewDustPerfect(position2 - (direction * 100), 131, Vector2.Zero, Projectile.alpha, Color.ForestGreen, 1f);
            }
            
            Projectile.ai[0] += 1f;
        }

        public override void Kill(int timeLeft) {
            if (Main.myPlayer == Projectile.owner) {
                for (int i = 0; i < NumberOfSpores; i++) {
                    Vector2 velocity = Main.rand.NextVector2Unit();
                    velocity *= 4;

                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, velocity, 
                        ModContent.ProjectileType<SylviaSpores>(), 1, 0, Projectile.owner);
                    TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: SporeDamage);

                    SylviaSpores spore = proj.ModProjectile as SylviaSpores;
                    if (spore != null) {
                        spore.SporeDuration = SporeDuration;
                    }
                }
            }
        }

        public override void SendExtraAI(BinaryWriter writer) {
            writer.Write(SporeDamage);
            writer.Write(NumberOfSpores);
            writer.Write(SporeDuration);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            SporeDamage = reader.ReadInt32();
            NumberOfSpores = reader.ReadInt32();
            SporeDuration = reader.ReadInt32();
        }
    }
}