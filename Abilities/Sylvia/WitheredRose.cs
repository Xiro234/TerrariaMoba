using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class WitheredRose : Ability, ITakePvpDamage {
        public WitheredRose(Player player) : base(player, "Withered Rose", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaAbilityTwo").Value;  }

        public const int THORN_DAMAGE = 200;

        private bool toggleOn;
        
        public override void OnCast() {
            //TODO - new sylvia ability
            //passive: passively grants 10% armor and mr penetration
            //toggle: surrounds sylvia in a thorn bush when damage is taken, fire a homing thorn that deals 50% of primary damage towards attacker as magic dmg, drains mana whilst on

            if (!toggleOn) {
                toggleOn = true;
                IsActive = true;
            }  else {
                toggleOn = false;
                IsActive = false;
            }

            CooldownTimer = BaseCooldown;
        }

        public override void WhileActive() {
            // armor/mr penetration code here
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            if (IsActive) {
                
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Vector2 direction = Vector2.UnitX;
                    Vector2 velocity = direction.RotatedBy(MathHelper.ToRadians(1 * 90 + 45));

                    Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), User.Center, velocity,
                        ModContent.ProjectileType<RoseThorn>(), 0, 0, User.whoAmI, killer);
                    TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: THORN_DAMAGE);
                }
                
                //drain mana code here
            }
        }
    }
}