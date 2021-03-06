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