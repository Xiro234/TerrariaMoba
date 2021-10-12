using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class EnsnaringVinesAbility : Ability, IModifyHitPvpWithProj {
        public EnsnaringVinesAbility() : base("Ensnaring Vines", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Sylvia/SylviaAbilityOne").Value; }
        
        public const int TRAP_BASE_DAMAGE = 225;
        public const int TRAP_BASE_DURATION = 90;
        public const int TRAP_BASE_NUMBER = 5;
        public const int TRAP_BASE_TILE_DISTANCE = 6;

        public const int ROOT_BASE_DURATION = 120;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - User.Center;

                int direction = Math.Sign((int) playerToMouse.X);
                Vector2 velocity = new Vector2(direction * 6, 0);

                Vector2 directionVector = Vector2.UnitX * direction;
                Vector2 position = User.Center + directionVector * TRAP_BASE_TILE_DISTANCE * 16;

                Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this),position, velocity, ModContent.ProjectileType<EnsnaringVinesSpawner>(), 
                    0, 0, User.whoAmI);
                
                EnsnaringVinesSpawner spawner = proj.ModProjectile as EnsnaringVinesSpawner;
                
                if (spawner != null) {
                    spawner.TrapDamage = TRAP_BASE_DAMAGE;
                    spawner.TrapDuration = TRAP_BASE_DURATION;
                    spawner.NumberOfTraps = TRAP_BASE_NUMBER;
                    spawner.TileDistance = TRAP_BASE_TILE_DISTANCE;
                }

                CooldownTimer = BaseCooldown;
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var ModProjectile = proj.ModProjectile;
            EnsnaringVinesTrap trap = ModProjectile as EnsnaringVinesTrap;
            if (trap != null) {
                StatusEffectManager.AddEffect(target, new EnsnaringVinesEffect(ROOT_BASE_DURATION, true));
            }
        }
    }
}