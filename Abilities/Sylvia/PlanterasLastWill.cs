using Terraria;
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
    public class PlanterasLastWill : Ability, IModifyHitPvpWithProj {
        public PlanterasLastWill(Player player) : base(player, "Plantera's Last Will", 180, 20, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaUltimateTwo").Value; }

        public const int HEAD_DAMAGE = 680;
        public const int SPORE_DAMAGE = 160;
        public const int SPORE_DURATION = 240;
        public const int SPORE_NUMBER = 6;

        public const int STUN_DURATION = 150;
        public const float HEAL_REDUCTION = 0.3f;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - User.Center;
                playerToMouse.Normalize();
                
                Vector2 position = User.Center + playerToMouse * 20;
                Vector2 velocity = playerToMouse * 7;

                Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this),position, velocity, ModContent.ProjectileType<SylviaUlt2>(), 
                    1, 0f, User.whoAmI);
                TerrariaMobaUtils.SetProjectileDamage(proj, PhysicalDamage: HEAD_DAMAGE);
                
                SylviaUlt2 head = proj.ModProjectile as SylviaUlt2;
                
                if (head != null) {
                    head.SporeDamage = SPORE_DAMAGE;
                    head.NumberOfSpores = SPORE_NUMBER;
                    head.SporeDuration = SPORE_DURATION;
                }

                CooldownTimer = BaseCooldown;
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var ModProjectile = proj.ModProjectile;
            SylviaUlt2 head = ModProjectile as SylviaUlt2;
            if (head != null) {
                StatusEffectManager.AddEffect(target, new PlanteraStunEffect(STUN_DURATION, true));
            }
            
            SylviaSpores spore = ModProjectile as SylviaSpores;
            if (spore != null) {
                //TODO - Add code for spores reducing healing effectiveness.
            }
        }
    }
}