using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class Rockwrecker : Ability {
        public Rockwrecker(Player player) : base(player, "Rockwrecker", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobAbilityThree").Value; }

        public const int ROCK_DAMAGE = 500;
        public const float ROCK_KNOCKBACK = 15f;
        public const int GUIDE_LIFETIME = 150;
        public const float ROCK_RANGE = 30f;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                //TODO - Lock user controls for a short duration (cast time)
                Vector2 position = new Vector2();
                if (User.direction < 0) {
                    position.X = User.Center.X - (ROCK_RANGE * 16.0f);
                } else {
                    position.X = User.Center.X + (ROCK_RANGE * 16.0f);
                }
                position.Y = GetYPos(position.X) - 13f;

                Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), 
                    position, Vector2.Zero, ModContent.ProjectileType<RockwreckerGuide>(), 1, 0, 
                    User.whoAmI, -1f, 360f);

                RockwreckerGuide guide = proj.ModProjectile as RockwreckerGuide;

                if (guide != null) { 
                    guide.RockDamage = ROCK_DAMAGE;
                    guide.RockKnockback = ROCK_KNOCKBACK;
                    guide.GuideTimer = GUIDE_LIFETIME;
                }
                
            }
        }
        
        public int GetYPos(float x) {
            int posX = (int)x;
            int posY = (int)User.Center.Y;

            if (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                while (TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY -= 1;
                }
            } else {
                while (!TerrariaMobaUtils.TileIsSolidOrPlatform(posX / 16, posY / 16)) {
                    posY += 1;
                }
            }
            return posY;
        }
    }
    
    
}