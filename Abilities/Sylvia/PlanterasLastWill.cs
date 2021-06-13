using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class PlanterasLastWill : Ability, IModifyHitPvpWithProj {
        public PlanterasLastWill() : base("Plantera's Last Will", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaUltimateTwo"); }

        public const int HEAD_BASE_DAMAGE = 680;
        public const int SPORE_BASE_DAMAGE = 160;
        public const int SPORE_BASE_DURATION = 240;
        public const int SPORE_BASE_NUMBER = 6;

        public const int STUN_BASE_DURATION = 150;
        public const float HEAL_BASE_REDUCTION = 0.3f;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 playerToMouse = Main.MouseWorld - User.Center;
                playerToMouse.Normalize();
                
                Vector2 position = User.Center + playerToMouse * 20;
                Vector2 velocity = playerToMouse * 7;

                Projectile proj = Projectile.NewProjectileDirect(position, velocity, TerrariaMoba.Instance.ProjectileType("SylviaUlt2"), 
                    HEAD_BASE_DAMAGE, 1, User.whoAmI);
                
                SylviaUlt2 head = proj.modProjectile as SylviaUlt2;
                
                if (head != null) {
                    head.SporeDamage = SPORE_BASE_DAMAGE;
                    head.NumberOfSpores = SPORE_BASE_NUMBER;
                    head.SporeDuration = SPORE_BASE_DURATION;
                }
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProjectile = proj.modProjectile;
            SylviaUlt2 head = modProjectile as SylviaUlt2;
            if (head != null) {
                StatusEffectManager.AddEffect(target, new PlanteraStunEffect(STUN_BASE_DURATION, true));
            }
            
            SylviaSpores spore = modProjectile as SylviaSpores;
            if (spore != null) {
                //TODO - Add code for spores reducing healing effectiveness.
            }
        }
    }
}
/*using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Sylvia {
    [Serializable]
    public class PlanterasLastWill : Ability {
        public PlanterasLastWill(Player myPlayer) : base(myPlayer) {
            Name = "Plantera's Last Will";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaUltimateTwo");
        }
        
        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 position = User.Center;
                Vector2 playerToMouse = Main.MouseWorld - User.Center;
                playerToMouse.Normalize();
                position += playerToMouse * 20;
                
                Vector2 velocity = playerToMouse * 7;

                Projectile proj = Main.projectile[Projectile.NewProjectile(position, velocity,
                    TerrariaMoba.Instance.ProjectileType("SylviaUlt2"), 
                    (int)User.GetModPlayer<MobaPlayer>().SylviaStats.U2HeadDmg.Value, 0, User.whoAmI)];
            }
            
            cooldownTimer = 20 * 60;
        }
    }
}*/