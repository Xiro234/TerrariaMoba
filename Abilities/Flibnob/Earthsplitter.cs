using System;
using Microsoft.Xna.Framework;
using Terraria;
using Tile = On.Terraria.Tile;

namespace TerrariaMoba.Abilities.Flibnob
{
    public class Earthsplitter : Ability
    {
        public Earthsplitter(Player myPlayer) : base(myPlayer) {
            Name = "Earthsplitter";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobUltimateOne");
        }

        public override void Cast() {
            player.velocity.Y = -14.6f;
            IsActive = true;
        }

        public override void Using() {
            if (player.velocity.Y == 0 && player.oldVelocity.Y == 0 && !player.mount.Active)
            {
                IsActive = false;
                Vector2 position = player.Center;

                if (player.direction < 0) {
                    position.X -= 110;
                } else {
                    position.X += 110;
                }
                Vector2 velocity = new Vector2(player.direction * 4f, 0);

                Projectile proj = Main.projectile[Projectile.NewProjectile(position, velocity, 
                    TerrariaMoba.Instance.ProjectileType("EarthsplitterSpawner"), 500, 0, player.whoAmI, 39f)];
                Cooldown = 20 * 60;
            }
        }
    }
}