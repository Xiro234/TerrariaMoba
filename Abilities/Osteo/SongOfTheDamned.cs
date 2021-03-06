/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Effects;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Osteo {
    [Serializable]
    public class SongOfTheDamned : Ability {
        public SongOfTheDamned(Player myPlayer) : base(myPlayer) {
            Name = "Song of the Damned";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Osteo/OsteoUltimateOne");
        }

        public override void OnCast() {
            IsActive = true;
            Timer = 0;
        }

        public override void WhileActive() {
            Timer++;
            for (int i = 0; i < Main.maxPlayers; i++) {
                if (Main.player[i] != null && Main.player[i].active) {
                    if (Main.player[i].team != User.team) {
                        var modPlayer = Main.player[i].GetModPlayer<MobaPlayer>();
                        if (!Main.player[i].dead) {
                            modPlayer.ultTimer = Timer;

                            double displace = OsteoEffects.GetSwordPosition(Timer);
                            float xPosition = Main.player[i].Center.X - 1; //Aligning

                            displace -= 34;

                            if (displace < 100) {
                                Dust dust = Dust.NewDustPerfect(
                                    new Vector2(xPosition, (float) (displace - 80 + Main.player[i].Center.Y)),
                                    59, Vector2.Zero, 1, default(Color), 1.6f); //Bobbing
                                dust.noGravity = true;
                            }
                        }
                        else {
                            modPlayer.ultTimer = -1;
                        }
                    }
                }
            }
            
            if (Timer > 180) {
                TimeOut();
            }
        }

        public override void TimeOut() {
            IsActive = false;
            for (int i = 0; i < Main.maxPlayers; i++) {
                if (Main.player[i] != null && Main.player[i].active) {
                    if (Main.player[i].team != User.team) {
                        var modPlayer = Main.player[i].GetModPlayer<MobaPlayer>();
                        if (!Main.player[i].dead) {
                            modPlayer.DamageOverride((int)User.GetModPlayer<MobaPlayer>().OsteoStats.U1Dmg.Value, Main.player[i], User.whoAmI, true);
                            Main.PlaySound(SoundID.Item71, Main.player[i].position);
                        }
                        modPlayer.ultTimer = -1;
                    }
                }
            }
        }
    }
}*/