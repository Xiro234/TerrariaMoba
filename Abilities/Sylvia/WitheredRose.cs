﻿using Microsoft.Xna.Framework;
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

        public const float THORN_DAMAGE = 200f;

        private bool toggleOn;
        
        public override void OnCast() {
            //TODO - new sylvia ability
            //passive: passively grants 10% armor and mr penetration
            //toggle: surrounds sylvia in a thorn bush when damage is taken, fire a homing thorn that deals 50% of primary damage towards attacker as magic dmg, drains mana whilst on

            if (!toggleOn) {
                toggleOn = true;
                IsActive = true;
                Main.NewText("Withered Rose: Toggle On.");
            }  else {
                toggleOn = false;
                IsActive = false;
                Main.NewText("Withered Rose: Toggle Off.");
            }

            CooldownTimer = BaseCooldown;
        }

        public override void WhileActive() {
            // armor/mr penetration code here
            //Main.NewText("Withered Rose: IsActive " + IsActive);
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            Main.NewText("Withered Rose: um " + IsActive);
            if (IsActive) {
                Main.NewText("Withered Rose: Damage taken whilst toggle on.");
                
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Vector2 direction = Vector2.UnitX;
                    Vector2 velocity = direction.RotatedBy(MathHelper.ToRadians(1 * 90 + 45));

                    Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), User.Center, velocity,
                        ModContent.ProjectileType<RoseThorn>(), (int)THORN_DAMAGE, 0, User.whoAmI, killer);
                    
                    Main.NewText("Withered Rose: Created projectile.");
                }
                
                //drain mana code here
            }
        }
    }
}