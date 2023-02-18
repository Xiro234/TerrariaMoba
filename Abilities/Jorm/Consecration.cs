using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Jorm;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Jorm;

namespace TerrariaMoba.Abilities.Jorm {
    public class Consecration : Ability, IModifyHitPvpWithProj {
        public Consecration(Player player) : base(player, "Consecration", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Jorm/JormAbilityTwo").Value; }

        public const float CONSEC_SPREAD_RANGE = 500f;
        public const int CONSEC_DURATION = 300;
        public const int CONSEC_DAMAGE = 100;

        public const int CONSEC_BUFF_DURATION = 180;
        public const float CONSEC_HEALEFF_MODIFIER = 0.25f;
        
        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Projectile proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this), 
                    User.Center, Vector2.Zero, ModContent.ProjectileType<ConsecrationProj>(), 
                    1, 0, User.whoAmI);
                TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: CONSEC_DAMAGE);

                ConsecrationProj consec = proj.ModProjectile as ConsecrationProj;
                if (consec != null) {
                    consec.ConsecSpread = CONSEC_SPREAD_RANGE;
                    consec.ConsecDuration = CONSEC_DURATION;
                }
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int phyiscalDamage, ref int magicalDamage, ref int trueDamage, ref bool crit) {
            var modProjectile = proj.ModProjectile;
            ConsecrationProj consec = modProjectile as ConsecrationProj;
            if (consec != null) {
                if (target.team != User.team) {
                    StatusEffectManager.AddEffect(target, new ConsecrationEffect(CONSEC_HEALEFF_MODIFIER, CONSEC_BUFF_DURATION, true, User.whoAmI));
                } else {
                    StatusEffectManager.AddEffect(target, new ConsecrationEffect(-CONSEC_HEALEFF_MODIFIER, CONSEC_BUFF_DURATION, true, User.whoAmI));
                }
            }
        }
    }
}