using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Osteo {
    public class LifedrainPulse : Ability {
        public LifedrainPulse(Player myPlayer) : base(myPlayer) {
            Name = "Lifedrain Pulse";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }

        public override void Cast() {
            Timer = 0;
            IsActive = true;
        }

        public override void Using() {
            if (Timer == 0 || Timer == 60) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                    for (int i = 0; i < 45; i++) {
                        double x = Math.Cos(((Math.PI / 180) * ((360 / 45) * i)));
                        double y = Math.Sin(((Math.PI / 180) * ((360 / 45) * i)));
                        Vector2 direction = new Vector2((float) x, (float) y);
                        Vector2 velocity = direction * 5;
                        Projectile.NewProjectile(player.Center, velocity,
                            TerrariaMoba.Instance.ProjectileType("LifedrainPulse"), 10, 0, player.whoAmI);
                    }
                }
            }
            else if (Timer == 120) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) {
                    for (int i = 0; i < 60; i++) {
                        double x = Math.Cos(((Math.PI / 180) * ((360 / 45) * i)));
                        double y = Math.Sin(((Math.PI / 180) * ((360 / 45) * i)));
                        Vector2 direction = new Vector2((float) x, (float) y);
                        Vector2 velocity = direction * 5;
                        Projectile.NewProjectile(player.Center, velocity,
                            TerrariaMoba.Instance.ProjectileType("LifedrainPulseThird"), 10, 0, player.whoAmI);
                    }
                }
                End();
            }


            Timer++;
        }

        public override void End() {
            IsActive = false;
            Cooldown = 60;
        }
    }
}