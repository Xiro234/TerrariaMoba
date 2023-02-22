using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles.OldMan;

namespace TerrariaMoba.Abilities.OldMan {
    public class HookPotato : Ability {
        public HookPotato(Player player) : base(player, "HookPotato", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock").Value; }

        public const int BASE_EXPLOSION_RADIUS = 10;
        
        public override void OnCast() {
            foreach (Projectile projectile in Main.projectile) {
                Bobber bobber = projectile.ModProjectile as Bobber;

                if (bobber != null) {
                    if (bobber.Projectile.owner == User.whoAmI) {
                        foreach (Player player in Main.player) {
                            if (player.team != User.team) {
                                if (Math.Abs((player.position - bobber.Projectile.position).Length()) <= (16f * 5f)) {
                                    player.GetModPlayer<MobaPlayer>().TakePvpDamage(100, 0, 0, User.whoAmI, true);
                                    
                                }
                            }
                        }
                        
                        			// Play explosion sound
						SoundEngine.PlaySound(SoundID.Item15, projectile.position);
						// Smoke Dust spawn
						for (int i = 0; i < 50; i++) {
							int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), BASE_EXPLOSION_RADIUS, BASE_EXPLOSION_RADIUS, DustID.Smoke, 0f, 0f, 100, default(Color), 1f);
							Main.dust[dustIndex].velocity *= 1.4f;
						}
                        
                        bobber.Detach();
                    }
                }
            }
        }
    }
}