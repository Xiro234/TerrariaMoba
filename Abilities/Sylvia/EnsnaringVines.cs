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
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class EnsnaringVines : Ability, IDealPvpDamage {
        public EnsnaringVines(Player player) : base(player, "Ensnaring Vines", 180, 20, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaAbilityOne").Value; }
        
        public const int TRAP_DAMAGE = 225;
        public const int TRAP_DURATION = 240;
        public const int TRAP_AMOUNT = 5;
        public const int TRAP_DISTANCE = 6;

        public const int ROOT_DURATION = 120;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - User.Center;

                int direction = Math.Sign((int) playerToMouse.X);
                Vector2 velocity = new Vector2(direction * 6, 0);

                Vector2 directionVector = Vector2.UnitX * direction;
                Vector2 position = User.Center + directionVector * TRAP_DISTANCE * 16;

                Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this), 
                    position, velocity, ModContent.ProjectileType<EnsnaringVinesSpawner>(), 1, 0, User.whoAmI);
                
                EnsnaringVinesSpawner spawner = proj.ModProjectile as EnsnaringVinesSpawner;
                
                if (spawner != null) {
                    spawner.TrapDamage = TRAP_DAMAGE;
                    spawner.TrapDuration = TRAP_DURATION;
                    spawner.NumberOfTraps = TRAP_AMOUNT;
                    spawner.TileDistance = TRAP_DISTANCE;
                }

                CooldownTimer = BaseCooldown;
            }
        }
        public void DealPvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, Player target, DamageSource damageSource) {
            if (damageSource.source is Projectile) {
                Projectile proj = damageSource.source as Projectile;

                var modProjectile = proj.ModProjectile;
                EnsnaringVinesTrap trap = modProjectile as EnsnaringVinesTrap;
                if (trap != null) {
                    StatusEffectManager.AddEffect(target, new EnsnaringVinesEffect(ROOT_DURATION, true, User.whoAmI));
                }
            }
        }
    }
}