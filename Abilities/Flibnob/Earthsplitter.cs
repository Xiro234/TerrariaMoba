using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Flibnob;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Abilities.Flibnob {
    public class Earthsplitter : Ability, IModifyHitPvpWithProj {
        public Earthsplitter(Player player) : base(player, "Earthsplitter", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobUltimateOne").Value; }

        public const float LEAP_BASE_HEIGHT = -14.6f;

        public const int EARTH_BASE_DAMAGE = 1000;
        public const int EARTH_BASE_NUMBER = 5;
        public const int EARTH_BASE_DURATION = 45;
        public const int EARTH_BASE_DELAY = 15;

        public override void OnCast() {
            User.velocity.Y = LEAP_BASE_HEIGHT;
            IsActive = true;
        }

        public override void WhileActive() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                if (User.velocity.Y == 0 && User.oldVelocity.Y == 0 && !User.mount.Active) {
                    IsActive = false;
                    Vector2 position = User.Center;
                    if (User.direction < 0) {
                        position.X -= 110;
                    } else {
                        position.X += 110;
                    }
                    Vector2 velocity = new Vector2(User.direction * 10f, 0);

                    Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), 
                        position, velocity, ModContent.ProjectileType<EarthsplitterSpawner>(), 1, 0,
                        User.whoAmI);
                    
                    EarthsplitterSpawner spawner = proj.ModProjectile as EarthsplitterSpawner;

                    if (spawner != null) {
                        spawner.EarthDamage = EARTH_BASE_DAMAGE;
                        spawner.NumberOfEarths = EARTH_BASE_NUMBER;
                        spawner.EarthDuration = EARTH_BASE_DURATION;
                        spawner.EarthDistance = EARTH_BASE_DELAY;
                    }
                }
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProjectile = proj.ModProjectile;
            SplitEarth earth = modProjectile as SplitEarth;
            if (earth != null) {
                StatusEffectManager.AddEffect(target, new FunStun(120, true));
            }
        }
    }
}