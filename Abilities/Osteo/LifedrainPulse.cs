using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles;

namespace TerrariaMoba.Abilities.Osteo {
    public class LifedrainPulse : Ability {
        public LifedrainPulse(Player player) : base(player, "Lifedrain Pulse", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoAbilityTwo").Value; }

        public const int PULSE_DAMAGE = 300;
        
        private int Timer;
        
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
                        var proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this), 
                            position, velocity, ModContent.ProjectileType<Projectiles.Osteo.LifedrainPulse>(), 0, 
                            0, User.whoAmI);
                        TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: PULSE_DAMAGE);
                        proj.timeLeft = 90;
                    }
                }
            } else if (Timer == 120) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    for (int i = 0; i < numProj; i++) {
                        double x = Math.Cos(((Math.PI / 180) * ((360f / numProj) * i)));
                        double y = Math.Sin(((Math.PI / 180) * ((360f / numProj) * i)));
                        Vector2 direction = new Vector2((float) x, (float) y);
                        Vector2 position = User.Center + direction * 16;
                        Vector2 velocity = direction * 6.25f;
                        var proj = Projectile.NewProjectileDirect(new EntitySource_Ability(User, this), 
                            position, velocity, ModContent.ProjectileType<Projectiles.Osteo.LifedrainPulse>(), 0, 
                            0, User.whoAmI);
                        TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: (int)(PULSE_DAMAGE * 1.5f));
                        proj.timeLeft = 90;
                    }
                }
                TimeOut();
            }
            Timer++;
        }

        public override void TimeOut() {
            IsActive = false;
            CooldownTimer = 60;
        }
    }
}