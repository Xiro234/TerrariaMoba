using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Nocturne;

namespace TerrariaMoba.Abilities.Nocturne {
    public class IronRush : Ability {
        public IronRush() : base("Iron Rush", 60, 0, AbilityType.Active) { }

        // TODO - Add an animation to make it look like the projectile is moving/running.
        // TODO - Make sprite alpha scale with Sin/Cos so it pulses in and out.
        // TODO - Replace control locking with a root near the final product (cant test on SP otherwise).
        // TODO - If possible, stop the teleport forcing Player into blocks (can be used to get out of bounds).
        // TODO - Implement armor boost effect.
        // TODO - Implement stun on enemy contact effect.
        // TODO - Implement damage, resource and scaling stats.
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public const float DASH_X_VELOCITY = 8f;
        public const int WAIT_TIME = 60;

        public Projectile dash = null;
        public int timer;
        
        public override void OnCast() {
            IsActive = true;
            timer = WAIT_TIME;
            
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 position = User.Center;
                position.Y -= 8f; // ups height of proj to avoid collision
                int direction = User.direction;

                Vector2 velocity = new Vector2(direction * DASH_X_VELOCITY, 0f);

                dash = Main.projectile[Projectile.NewProjectile(new ProjectileSource_Ability(User, this), position, velocity,
                    ModContent.ProjectileType<NocturneDash>(),
                    0, 0, User.whoAmI)];
            }
        }

        public override void WhileActive() {
            timer--;
            
            if (timer > 0) {
                User.controlLeft = false;
                User.controlRight = false;
                User.controlJump = false;
                User.controlUp = false;
            }

            if (timer == 0 || !dash.active) {
                User.controlLeft = true;
                User.controlRight = true;
                User.controlJump = true;
                User.controlUp = true;
                
                User.position = dash.position;
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, Main.myPlayer);
                dash.Kill();
                
                TimeOut();
            }
        }

        public override void TimeOut() {
            timer = 0;
            IsActive = false;
        }
    }
}