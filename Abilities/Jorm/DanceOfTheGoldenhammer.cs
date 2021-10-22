using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Jorm;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Jorm;

namespace TerrariaMoba.Abilities.Jorm {
    public class DanceOfTheGoldenhammer : Ability {
        public DanceOfTheGoldenhammer() : base("Dance of the Goldenhammer", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        public const float HAMMER_DAMAGE = 200f;
        public const float HAMMER_SPIN_RADIUS = 135f;
        public const float HAMMER_SPAWN_SPEED = 5f;
        public const float DAZE_MAGNITUDE = 0.2f;
        public const int DAZE_BASE_DURATION = 90;
        
        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                PaladinsResolve pr = User.GetModPlayer<MobaPlayer>().Hero.Trait as PaladinsResolve;
                if (pr != null) {
                    pr.AddStack();
                }

                for (int i = 0; i < 4; i++) {
                    Vector2 direction = Vector2.UnitX;

                    Vector2 velocity = direction.RotatedBy(MathHelper.ToRadians(i * 90 + 45));

                    Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), User.Center, velocity,
                        ModContent.ProjectileType<SpinningHammer>(), (int)HAMMER_DAMAGE, 0, User.whoAmI);
                    
                    SpinningHammer hammer = proj.ModProjectile as SpinningHammer;
                    if (hammer != null) {
                        hammer.SpinRadius = HAMMER_SPIN_RADIUS;
                        hammer.SpawnSpeed = HAMMER_SPAWN_SPEED;
                    }
                }

                SoundEngine.PlaySound(SoundID.Item1, User.Center);
            }
        }
        
        //TODO - Not dealing damage to NPCs for some reason (Hammerfall does for example)
        
        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProjectile = proj.ModProjectile;
            SpinningHammer trap = modProjectile as SpinningHammer;
            if (trap != null) {
                StatusEffectManager.AddEffect(target, new GoldenhammerDanceEffect(DAZE_MAGNITUDE, DAZE_BASE_DURATION, true));
            }
        }
    }
}