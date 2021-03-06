/*using Terraria;
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Sylvia {
    [Serializable]
    public class Flourish : Ability {
        public int NumberJavelins = 0;
        private int PreviousJavelins = 0;
        public Projectile teleport = null;
        public bool teleporting = false;

        public Flourish(Player myPlayer) : base(myPlayer) {
            Name = "Flourish";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaUltimateOne");
        }

        public override void OnCast() {
            Timer = 6 * 60;
            NumberJavelins = 3;
            IsActive = true;
            teleporting = true;
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {

                Vector2 position = User.Top;
                Vector2 playerToMouse = Main.MouseWorld - User.Center;
                int direction = -Math.Sign((int) playerToMouse.X);

                Vector2 velocity = new Vector2(direction * 0.5f, -0.866f); //Unit vector in specific direction
                velocity *= 12;

                teleport = Main.projectile[Projectile.NewProjectile(position, velocity,
                    TerrariaMoba.Instance.ProjectileType("SylviaUlt1Teleport"),
                    0, 0, User.whoAmI)];
            }
        }

        public override void WhileActive() {
            base.WhileActive();
            Timer--;

            if (Timer > 345) {
                User.immune = true;
                User.immuneTime = 1;
            }
            else if (Timer == 345) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    User.position = teleport.position;
                    NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, Main.myPlayer);
                    teleport.Kill();
                }
                teleporting = false;
                NumberJavelins = 3;
                PreviousJavelins = 3;
            }
            else if (Timer < 345) {
                if (User.velocity.Y != 0f) { //Ripped from webbed
                    User.velocity = new Vector2(0f, 1E-06f);
                }
                else {
                    User.velocity = Vector2.Zero;
                }

                User.gravity = 0f;
                User.moveSpeed = 0f;
            }
            if (Timer == 0 || NumberJavelins == 0) {
                TimeOut();
            }
            PreviousJavelins = NumberJavelins;
        }

        public override void TimeOut() {
            Timer = 0;
            NumberJavelins = 0;
            IsActive = false;
            teleporting = false;
            cooldownTimer = 20 * 60;
        }
    }
}*/