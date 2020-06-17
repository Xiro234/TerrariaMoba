using Terraria;
using System;
using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.Abilities.Sylvia {
    public class EnsnaringVines : Ability {
        public EnsnaringVines(Player myPlayer) : base(myPlayer) {
            Name = "Ensnaring Vines";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaAbilityOne");
        }

        public override void OnCast() {
            Vector2 position = player.Center;
            Vector2 playerToMouse = Main.MouseWorld - player.Center;

            int direction = Math.Sign((int) playerToMouse.X);
            Vector2 velocity = new Vector2(direction * 6, 0);

            Projectile proj = Main.projectile[Projectile.NewProjectile(position, velocity,
                TerrariaMoba.Instance.ProjectileType("EnsnaringVinesSpawner"), 30, 0, player.whoAmI)];
            Cooldown = 40 * 60;
        }
    }
}