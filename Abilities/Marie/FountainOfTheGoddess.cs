using Microsoft.Xna.Framework;
using Terraria;

namespace TerrariaMoba.Abilities.Marie {
    public class FountainOfTheGoddess : Ability {
        public FountainOfTheGoddess(Player myPlayer) : base(myPlayer) {
            Name = "Fountain of the Goddess";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieUltimateOne");
        }
        
        public override void OnCast() {
            Vector2 position = Main.LocalPlayer.Center;
            Projectile proj = Main.projectile[Projectile.NewProjectile(position, Vector2.Zero, 
                TerrariaMoba.Instance.ProjectileType("FountainOfLacusia"), 0, 0, Main.LocalPlayer.whoAmI, 29f)];
            Cooldown = 10 * 60;
        }
    }
}