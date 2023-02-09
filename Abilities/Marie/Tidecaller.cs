using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Marie;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Abilities.Marie {
    public class Tidecaller : Ability, IModifyHitPvpWithProj {
        public Tidecaller(Player player) : base(player, "Tidecaller", 60, 1, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieAbilityOne").Value; }

        public const int TIDE_DAMAGE = 222;
        public const int TIDE_DURATION = 12;
        public const int HEAL_VALUE = 100;
        public const int AIRTIME_DURATION = 90;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                int dir = User.direction;
                Vector2 velocity = new Vector2(dir * 5.3f, 5.3f);
                Vector2 spawnLoc = new Vector2(User.Top.X, User.Top.Y + -333f);

                Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this), spawnLoc, velocity, 
                    ModContent.ProjectileType<TidecallerProj>(), 1, 0, User.whoAmI);
                TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: 0);
                SoundEngine.PlaySound(SoundID.Item1, User.Center);

                TidecallerProj tide = proj.ModProjectile as TidecallerProj;
                if (tide != null) {
                    tide.TideDamage = TIDE_DAMAGE;
                    tide.TideDuration = TIDE_DURATION;
                }
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            // knock enemies up for 1.5s, heal marie for HEAL_VALUE
        }
    }
}