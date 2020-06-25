using Microsoft.Xna.Framework;
using Terraria;

namespace TerrariaMoba.Abilities.Flibnob
{
    public class CullTheMeek : Ability
    {
        public CullTheMeek(Player myPlayer) : base(myPlayer) {
            Name = "Cull the Meek";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobUltimateTwo");
        }
        
        public override void Cast()
        {
            Vector2 position = Main.LocalPlayer.Center;
            Projectile proj = Main.projectile[Projectile.NewProjectile(position, Vector2.Zero, 
                TerrariaMoba.Instance.ProjectileType("CullPillar"), 0, 0, Main.LocalPlayer.whoAmI, 0f)];
        }
    }
}