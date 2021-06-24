using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles.Jorm;

namespace TerrariaMoba.Abilities.Jorm {
    public class DanceOfTheGoldenhammer : Ability {
        public DanceOfTheGoldenhammer() : base("Dance of the Goldenhammer", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public const float HAMMER_SPIN_RADIUS = 135f;
        public const float HAMMER_SPAWN_SPEED = 5f;
        
        public override void OnCast() {
            //TODO - 4 hammers spin around him, damage and daze on hit (they break on collide).
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                for (int i = 0; i < 4; i++) {
                    Vector2 direction = Vector2.UnitX;

                    Vector2 velocity = direction.RotatedBy(MathHelper.ToRadians(i * 90 + 45));

                    Projectile projectile = Projectile.NewProjectileDirect(User.Center, velocity,
                        TerrariaMoba.Instance.ProjectileType("SpinningHammer"), 0, 0, User.whoAmI);
                    
                    SpinningHammer hammer = projectile.modProjectile as SpinningHammer;
                    if (hammer != null) {
                        hammer.SpinRadius = HAMMER_SPIN_RADIUS;
                    }
                }

                Main.PlaySound(SoundID.Item1, User.Center);
            }
        }
    }
}