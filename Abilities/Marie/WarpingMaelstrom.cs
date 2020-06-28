using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Marie {
    public class WarpingMaelstrom : Ability {
        public WarpingMaelstrom(Player myPlayer) : base(myPlayer) {
            Name = "Warping Maelstrom";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieAbilityOne");
        }

        public override void Cast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                Vector2 position = player.Center;
                Vector2 playerToMouse = Main.MouseWorld - player.Center;

                int directionX = Math.Sign((int) playerToMouse.X);
                int directionY = Math.Sign((int) playerToMouse.Y);
                if (directionY == 1) directionY = -1;
                Vector2 velocity = new Vector2(directionX * 4, directionY * 4);

                Projectile proj = Main.projectile[Projectile.NewProjectile(position, velocity,
                    TerrariaMoba.Instance.ProjectileType("Maelstrom"), 100, 1, player.whoAmI)];
                Main.NewText("yeet");
            }

            Main.PlaySound(SoundID.Item66, player.Center);
            Cooldown = 10 * 60;
        }
    }
}