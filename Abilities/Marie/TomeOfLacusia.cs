using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Marie {
    public class TomeOfLacusia : Ability {
        public TomeOfLacusia(Player myPlayer) : base(myPlayer) {
            Name = "Tome of Lacusia";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieAbilityTwo");
        }

        public override void Cast() {
            Timer = (int) (2.5 * 60);
            IsActive = true;
            player.AddBuff(BuffType<Buffs.Channeling>(), Timer);
        }

        public override void Using() {
            Timer--;
            if (Timer == 0) {
                End();
            }
        }

        public override void End() {
            Timer = 0;
            IsActive = false;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active) {
                    if (plr.team == player.team) {
                        plr.statLife += 100;
                        CombatText.NewText(plr.Hitbox, Color.CornflowerBlue, 100, true);
                        Main.PlaySound(SoundID.Item4, plr.Center);
                        for (int d = 0; d < 40; d++) {
                            Dust.NewDust(plr.position, plr.width, plr.height, 41, 0f, 0f, 150, default(Color), 1.5f);
                        }
                    }
                }
            }
            Cooldown = 10 * 60;
        }
    }
}