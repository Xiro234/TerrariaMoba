using Microsoft.Xna.Framework;
using Terraria;

namespace TerrariaMoba.Abilities.Marie {
    public class FountainOfTheGoddess : Ability {
        public FountainOfTheGoddess(Player myPlayer) : base(myPlayer) {
            Name = "Fountain of the Goddess";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieUltimateOne");
        }
        
        public override void Cast() {
            Projectile proj = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, 
                TerrariaMoba.Instance.ProjectileType("FountainOfLacusia"), 0, 0, player.whoAmI, 29f)];
            Cooldown = 10 * 60;
        }
    }
}