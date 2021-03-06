/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Flibnob {
    [Serializable]
    public class Earthsplitter : Ability {
        public Earthsplitter(Player myPlayer) : base(myPlayer) {
            Name = "Earthsplitter";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobUltimateOne");
        }

        public override void Cast() {
            User.velocity.Y = -14.6f;
            IsActive = true;
        }

        public override void WhileActive() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                if (User.velocity.Y == 0 && User.oldVelocity.Y == 0 && !User.mount.Active) {
                    IsActive = false;
                    Vector2 position = User.Center;
                    if (User.direction < 0) {
                        position.X -= 110;
                    } else {
                        position.X += 110;
                    }
                    Vector2 velocity = new Vector2(User.direction * 10f, 0);

                    Projectile.NewProjectile(position, velocity, 
                        TerrariaMoba.Instance.ProjectileType("EarthsplitterSpawner"), 
                        (int)User.GetModPlayer<MobaPlayer>().FlibnobStats.U2EarthDmg.Value, 0, User.whoAmI, 14f);
                }

                cooldownTimer = 20 * 60;
            }
        }
    }
}*/