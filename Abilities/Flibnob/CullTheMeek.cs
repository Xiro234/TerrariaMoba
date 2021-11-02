using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class CullTheMeek : Ability {
        public CullTheMeek() : base("Cull The Meek", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobUltimateTwo").Value; }

        public const float HOOK_BASE_RANGE = 25f;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this),User.Center, Vector2.Zero,
                    ModContent.ProjectileType<CullPillar>(), 0, 0, User.whoAmI);
                CullPillar pillar = proj.ModProjectile as CullPillar;

                if (pillar != null) {
                    pillar.HookRange = HOOK_BASE_RANGE;
                }
            }
        }
    }
}