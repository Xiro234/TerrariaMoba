using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Flibnob {
    public class CullTheMeek : Ability {
        public CullTheMeek(Player myPlayer) : base(myPlayer) {
            Name = "Cull the Meek";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobUltimateTwo");
        }
        
        public override void Cast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                Projectile.NewProjectile(player.Center, Vector2.Zero,
                    TerrariaMoba.Instance.ProjectileType("CullPillar"), 0, 0, player.whoAmI, 0f);
            }
        }
    }
}