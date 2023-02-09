using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Jorm;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Abilities.Jorm {
    public class Hammerfall : Ability {
        public Hammerfall(Player player) : base(player, "Hammerfall", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Jorm/JormUltimateOne").Value; }

        public const float BIGHAMMER_SPEED = 8f;
        public const int BIGHAMMER_DAMAGE = 700;
        public const float BIGHAMMER_HEIGHT = -600f;
        public const int BIGHAMMER_NUMBER = 4;
        public const int BIGHAMMER_TILE_DISTANCE = 18;
        public const int STUN_DURATION = 150;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                PaladinsResolve pr = User.GetModPlayer<MobaPlayer>().Hero.Trait as PaladinsResolve;
                if (pr != null) {
                    pr.AddStack();
                }
                
                int dir = User.direction;
                Vector2 velocity = new Vector2(dir * 15, 0);
                Vector2 spawnLoc = new Vector2(User.Top.X, User.Top.Y + BIGHAMMER_HEIGHT);
                
                Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), spawnLoc, velocity, 
                    ModContent.ProjectileType<HammerfallProjSpawner>(), 1, 0, User.whoAmI, 1);

                HammerfallProjSpawner spawner = proj.ModProjectile as HammerfallProjSpawner;
                
                if (spawner != null) {
                    spawner.HammerDamage = BIGHAMMER_DAMAGE;
                    spawner.HammerSpeed = BIGHAMMER_SPEED;
                    spawner.NumberOfHammers = BIGHAMMER_NUMBER;
                    spawner.TileDistance = BIGHAMMER_TILE_DISTANCE;
                }

                SoundEngine.PlaySound(SoundID.Item1, User.Center);
            }
        }
        
        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProjectile = proj.ModProjectile;
            HammerfallProj hammer = modProjectile as HammerfallProj;
            if (hammer != null) {
                StatusEffectManager.AddEffect(target, new FunStun(STUN_DURATION, true, User.whoAmI));
            }
        }
    }
}