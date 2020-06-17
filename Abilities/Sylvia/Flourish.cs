using Terraria;
using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Sylvia {
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
            
            Vector2 position = player.Top;
            Vector2 playerToMouse = Main.MouseWorld - player.Center;
            int direction = -Math.Sign((int) playerToMouse.X);

            Vector2 velocity = new Vector2(direction * 0.5f, -0.866f); //Unit vector in specific direction
            velocity *= 12;

            teleport = Main.projectile[Projectile.NewProjectile(position, velocity, TerrariaMoba.Instance.ProjectileType("SylviaUlt1Teleport"),
                0, 0, player.whoAmI)];
        }

        public override void InUse() {
            base.InUse();
            Timer--;

            if (Timer > 345) {
                player.immune = true;
                player.immuneTime = 1;
            }
            else if (Timer == 345) {
                player.Teleport(teleport.position, -1);
                teleport.Kill();
                teleporting = false;
                NumberJavelins = 3;
                PreviousJavelins = 3;
            }
            else if (Timer < 345) {
                if (player.velocity.Y != 0f) { //Ripped from webbed
                    player.velocity = new Vector2(0f, 1E-06f);
                }
                else {
                    player.velocity = Vector2.Zero;
                }

                player.gravity = 0f;
                player.moveSpeed = 0f;
            }
            if (Timer == 0 || NumberJavelins == 0) {
                OnEnd();
            }
            Main.NewText(NumberJavelins + " : " + PreviousJavelins);
            PreviousJavelins = NumberJavelins;
        }

        public override void OnEnd() {
            Timer = 0;
            NumberJavelins = 0;
            IsActive = false;
            teleporting = false;
            Cooldown = 30 * 60;
        }

        public override void ReadAbility(MemoryStream stream) {
            BinaryReader reader = new BinaryReader(stream);
            NumberJavelins = reader.ReadInt32();
        }

        public override byte[] WriteAbility() {
            return BitConverter.GetBytes(NumberJavelins);
        }

        public override void CheckSync() {
            if (NumberJavelins != PreviousJavelins) {
                NeedsSyncing = true;
            }
            else {
                NeedsSyncing = false;
            }
        }
    }
}