/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Marie {
    [Serializable]
    public class FountainOfTheGoddess : Ability {
        public FountainOfTheGoddess(Player myPlayer) : base(myPlayer) {
            Name = "Fountain of the Goddess";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieUltimateOne");
        }
        
        public override void Cast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Projectile.NewProjectile(User.Center, Vector2.Zero,
                    TerrariaMoba.Instance.ProjectileType("FountainOfLacusia"), 0, 0, User.whoAmI, 29f);
            }

            cooldownTimer = 20 * 60;
        }
    }
}*/