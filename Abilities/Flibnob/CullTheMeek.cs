using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class CullTheMeek : Ability, IModifyHitPvpWithProj {
        public CullTheMeek() : base("Cull The Meek", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobUltimateTwo"); }

        public const float HOOK_BASE_RANGE = 25f;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Projectile proj = Projectile.NewProjectileDirect(User.Center, Vector2.Zero,
                    TerrariaMoba.Instance.ProjectileType("CullPillar"), 0, 0, User.whoAmI);
                
                CullPillar pillar = proj.modProjectile as CullPillar;

                if (pillar != null) {
                    pillar.HookRange = HOOK_BASE_RANGE;
                }
            }
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            //TODO - Whatever debuff this will inflict goes here.
        }
    }
}
/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Flibnob {
    [Serializable]
    public class CullTheMeek : Ability {
        public CullTheMeek(Player myPlayer) : base(myPlayer) {
            Name = "Cull the Meek";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobUltimateTwo");
        }
        
        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Projectile.NewProjectile(User.Center, Vector2.Zero,
                    TerrariaMoba.Instance.ProjectileType("CullPillar"), 0, 0, User.whoAmI, 0f);
            }
        }
    }
}*/