using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Marie {
    public class TomeOfLacusia : Ability {
        public TomeOfLacusia() : base("Tome of Lacusia", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieAbilityTwo"); }

        public const int TOME_BASE_HEAL = 400;
        public const int TOME_BASE_CAST_TIME = 150;

        public int timer = 0;

        public override void OnCast() {
            timer = TOME_BASE_CAST_TIME;
            IsActive = true;
        }

        public override void WhileActive() {
            timer--;
            if (timer == 0) {
                TimeOut();
            }
        }

        public override void TimeOut() {
            IsActive = false;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active) {
                    if (plr.team == User.team) {
                        plr.statLife += TOME_BASE_HEAL;
                        CombatText.NewText(plr.Hitbox, Color.CornflowerBlue, TOME_BASE_HEAL, true);
                        Main.PlaySound(SoundID.Item4, plr.Center);
                        for (int d = 0; d < 40; d++) {
                            Dust.NewDust(plr.position, plr.width, plr.height, 41, 0f, 0f, 150, default(Color), 1.5f);
                        }
                    }
                }
            }
        }
    }
}

/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Players;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Marie {
    [Serializable]
    public class TomeOfLacusia : Ability {
        public TomeOfLacusia(Player myPlayer) : base(myPlayer) {
            Name = "Tome of Lacusia";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieAbilityTwo");
        }

        public override void OnCast() {
            Timer = (int) (2.5 * 60);
            IsActive = true;
            User.AddBuff(BuffType<Buffs.Channeling>(), Timer);
        }

        public override void WhileActive() {
            Timer--;
            if (Timer == 0) {
                TimeOut();
            }
        }

        public override void TimeOut() {
            Timer = 0;
            IsActive = false;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active) {
                    if (plr.team == User.team) {
                        plr.statLife += (int)User.GetModPlayer<MobaPlayer>().MarieStats.A2Heal.Value;
                        CombatText.NewText(plr.Hitbox, Color.CornflowerBlue, (int)User.GetModPlayer<MobaPlayer>().MarieStats.A2Heal.Value, true);
                        Main.PlaySound(SoundID.Item4, plr.Center);
                        for (int d = 0; d < 40; d++) {
                            Dust.NewDust(plr.position, plr.width, plr.height, 41, 0f, 0f, 150, default(Color), 1.5f);
                        }
                    }
                }
            }
            cooldownTimer = 10 * 60;
        }
    }
}*/