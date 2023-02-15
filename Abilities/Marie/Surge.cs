﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Marie {
    public class Surge : Ability {
        public Surge(Player player) : base(player, "Surge", 600, 50, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieTrait").Value; }

        public const int SURGE_HEAL = 750;
        
        public override void OnCast() {
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active && plr.team == User.team) {
                    if (User.whoAmI == plr.whoAmI) {
                        User.GetModPlayer<MobaPlayer>().HealMe(SURGE_HEAL, true);
                    } else {
                        User.GetModPlayer<MobaPlayer>().HealOtherPlayer(plr, SURGE_HEAL, true);
                    }
                    SoundEngine.PlaySound(SoundID.Item4, plr.Center);
                    for (int d = 0; d < 30; d++) {
                        Dust.NewDust(plr.position, plr.width, plr.height, DustID.Water_Hallowed, 0f, -2f, 170, default(Color), 2.5f);
                    }
                }
            }

            CooldownTimer = BaseCooldown;
        }
    }
}