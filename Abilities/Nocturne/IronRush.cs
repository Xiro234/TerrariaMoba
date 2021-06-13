using System;
using Microsoft.SqlServer.Server;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class IronRush : Ability {
        public IronRush() : base("Iron Rush", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public const float DASH_X_VELOCITY = 7f;
        public const int WAIT_TIME = 120;

        public Projectile dash = null;
        public int timer;
        
        public override void OnCast() {
            IsActive = true;
            timer = WAIT_TIME;
            
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                // get user position, find direction, fire projectile where player facing, teleport to projectile on its death
                Vector2 position = User.Top;
                int direction = User.direction;

                Vector2 velocity = new Vector2(direction * DASH_X_VELOCITY, 0f);

                dash = Main.projectile[Projectile.NewProjectile(position, velocity,
                    TerrariaMoba.Instance.ProjectileType("NocturneDash"),
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

            if (timer == 0) {
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