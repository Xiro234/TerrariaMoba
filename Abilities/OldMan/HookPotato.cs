using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles.OldMan;

namespace TerrariaMoba.Abilities.OldMan {
    public class HookPotato : Ability {
        public HookPotato(Player player) : base(player, "HookPotato", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

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
                    }
                }
                
            }
        }
    }
}