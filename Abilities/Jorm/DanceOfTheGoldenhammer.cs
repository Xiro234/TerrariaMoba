using System;
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
                Vector2 spawnSpeed = new Vector2(-HAMMER_SPAWN_SPEED + User.velocity.X, -HAMMER_SPAWN_SPEED + User.velocity.Y);
                Projectile proj1 = Projectile.NewProjectileDirect(User.Center, spawnSpeed,
                    TerrariaMoba.Instance.ProjectileType("SpinningHammer"), 0, 0, User.whoAmI);
                
                SpinningHammer hammer1 = proj1.modProjectile as SpinningHammer;
                if (hammer1 != null) {
                    hammer1.SpinRadius = HAMMER_SPIN_RADIUS;
                }
                
                spawnSpeed = new Vector2(-HAMMER_SPAWN_SPEED + User.velocity.X, HAMMER_SPAWN_SPEED + User.velocity.Y);
                Projectile proj2 = Projectile.NewProjectileDirect(User.Center, spawnSpeed,
                    TerrariaMoba.Instance.ProjectileType("SpinningHammer"), 0, 0, User.whoAmI);
                
                SpinningHammer hammer2 = proj2.modProjectile as SpinningHammer;
                if (hammer2 != null) {
                    hammer2.SpinRadius = HAMMER_SPIN_RADIUS;
                }
                
                spawnSpeed = new Vector2(HAMMER_SPAWN_SPEED + User.velocity.X, -HAMMER_SPAWN_SPEED + User.velocity.Y);
                Projectile proj3 = Projectile.NewProjectileDirect(User.Center, spawnSpeed,
                    TerrariaMoba.Instance.ProjectileType("SpinningHammer"), 0, 0, User.whoAmI);
                
                SpinningHammer hammer3 = proj3.modProjectile as SpinningHammer;
                if (hammer3 != null) {
                    hammer3.SpinRadius = HAMMER_SPIN_RADIUS;
                }
                
                spawnSpeed = new Vector2(HAMMER_SPAWN_SPEED + User.velocity.X, HAMMER_SPAWN_SPEED + User.velocity.Y);
                Projectile proj4 = Projectile.NewProjectileDirect(User.Center, spawnSpeed,
                    TerrariaMoba.Instance.ProjectileType("SpinningHammer"), 0, 0, User.whoAmI);
                
                SpinningHammer hammer4 = proj4.modProjectile as SpinningHammer;
                if (hammer4 != null) {
                    hammer4.SpinRadius = HAMMER_SPIN_RADIUS;
                }

                Main.PlaySound(SoundID.Item1, User.Center);
            }
        }
    }
}