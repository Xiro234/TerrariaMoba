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