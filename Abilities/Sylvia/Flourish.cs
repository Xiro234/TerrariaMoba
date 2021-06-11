using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Sylvia {
    public class Flourish : Ability {
        public Flourish() : base("Flourish", 60, 0, AbilityType.Active) {
        }

        public override Texture2D Icon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaAbilityOne");
        }

        public const int JAVELIN_BASE_DAMAGE = 400;
        public const int JAVELIN_BASE_NUMBER = 3;
        public const int AIRTIME_BASE_DURATION = 360;

        public Projectile teleport = null;
        public int timer;
        public int remainingJavelins;

        public override void OnCast() {
            IsActive = true;
            timer = AIRTIME_BASE_DURATION;
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
            timer--;

            if (timer > 345) {
                User.immune = true;
                User.immuneTime = 1;
            }
            else if (timer == 345) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    User.position = teleport.position;
                    NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, Main.myPlayer);
                    teleport.Kill();
                }

                remainingJavelins = JAVELIN_BASE_NUMBER;
            }
            else if (timer < 345) {
                if (User.velocity.Y != 0f) { //Ripped from webbed
                    User.velocity = new Vector2(0f, 1E-06f);
                }
                else {
                    User.velocity = Vector2.Zero;
                }

                User.gravity = 0f;
                User.moveSpeed = 0f;
            }

            if (timer == 0 || remainingJavelins == 0) {
                TimeOut();
            }
        }

        public override void TimeOut() {
            timer = 0;
            remainingJavelins = 0;
            IsActive = false;
        }
    }
}

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