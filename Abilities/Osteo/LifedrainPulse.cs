using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Osteo {
    public class LifedrainPulse : Ability {
        public LifedrainPulse() : base("Lifedrain Pulse", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoAbilityTwo").Value; }
    }
}

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
            Icon = ModContent.Request<Texture2D>("Textures/Osteo/OsteoAbilityTwo").Value;
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
                        var proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this),position, velocity,
                            ModContent.ProjectileType<LifedrainPulse"), (int)User.GetModPlayer<MobaPlayer>().OsteoStats.A2Dmg.Value, 0, User.whoAmI);

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
                        var proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this),position, velocity,
                            ModContent.ProjectileType<LifedrainPulseThird"), (int)(User.GetModPlayer<MobaPlayer>().OsteoStats.A2Dmg.Value * 1.66f), 0, User.whoAmI);
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