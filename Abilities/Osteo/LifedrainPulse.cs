/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Osteo {
    [Serializable]
    public class LifedrainPulse : Ability {
        public LifedrainPulse(Player myPlayer) : base(myPlayer) {
            Name = "Lifedrain Pulse";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Osteo/OsteoAbilityTwo");
        }

        public override void OnCast() {
            Timer = 0;
            IsActive = true;
        }

        public override void WhileActive() {
            int numProj = 8;
            if (Timer == 0 || Timer == 60) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    for (int i = 0; i < numProj; i++) {
                        double x = Math.Cos(((Math.PI / 180) * ((360f / numProj) * i)));
                        double y = Math.Sin(((Math.PI / 180) * ((360f / numProj) * i)));
                        Vector2 direction = new Vector2((float) x, (float) y);
                        Vector2 position = User.Center + direction * 16;
                        Vector2 velocity = direction * 6.25f;
                        var proj = Projectile.NewProjectileDirect(position, velocity,
                            TerrariaMoba.Instance.ProjectileType("LifedrainPulse"), (int)User.GetModPlayer<MobaPlayer>().OsteoStats.A2Dmg.Value, 0, User.whoAmI);

                        proj.timeLeft = 90;
                    }
                }
            }
            else if (Timer == 120) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    for (int i = 0; i < numProj; i++) {
                        double x = Math.Cos(((Math.PI / 180) * ((360f / numProj) * i)));
                        double y = Math.Sin(((Math.PI / 180) * ((360f / numProj) * i)));
                        Vector2 direction = new Vector2((float) x, (float) y);
                        Vector2 position = User.Center + direction * 16;
                        Vector2 velocity = direction * 6.25f;
                        var proj = Projectile.NewProjectileDirect(position, velocity,
                            TerrariaMoba.Instance.ProjectileType("LifedrainPulseThird"), (int)(User.GetModPlayer<MobaPlayer>().OsteoStats.A2Dmg.Value * 1.66f), 0, User.whoAmI);
                        proj.timeLeft = 90;
                    }
                }
                TimeOut();
            }
            
            Timer++;
        }

        public override void TimeOut() {
            IsActive = false;
            cooldownTimer = 60;
        }
    }
}*/