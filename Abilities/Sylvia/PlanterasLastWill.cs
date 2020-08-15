using Terraria;
using System;
using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Sylvia {
    public class PlanterasLastWill : Ability {
        public PlanterasLastWill(Player myPlayer) : base(myPlayer) {
            Name = "Plantera's Last Will";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaUltimateTwo");
        }
        
        public override void Cast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                Vector2 position = player.Center;
                Vector2 playerToMouse = Main.MouseWorld - player.Center;
                playerToMouse.Normalize();
                position += playerToMouse * 20;
                
                Vector2 velocity = playerToMouse * 7;

                Projectile proj = Main.projectile[Projectile.NewProjectile(position, velocity,
                    TerrariaMoba.Instance.ProjectileType("SylviaUlt2"), 680, 0, player.whoAmI)];
            }
            
            Cooldown = 20 * 60;
        }
    }
}