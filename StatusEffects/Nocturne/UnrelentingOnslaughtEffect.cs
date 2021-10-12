using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;

namespace TerrariaMoba.StatusEffects.Nocturne {
    public class UnrelentingOnslaughtEffect: StatusEffect, IShoot {
        public override string DisplayName { get => "Unrelenting Onslaught"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }
        
        public UnrelentingOnslaughtEffect() { }
        
        public UnrelentingOnslaughtEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        public bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {

            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                SoundEngine.PlaySound(SoundID.Item114, User.Center);
                float mult = 2f;
                Vector2 velocity = new Vector2(speedX, speedY);
                Projectile.NewProjectile(User.Center + velocity, velocity, ProjectileID.LightBeam, (int) (damage * mult),
                    0, User.whoAmI);
            }

            return false;
        }
    }
}