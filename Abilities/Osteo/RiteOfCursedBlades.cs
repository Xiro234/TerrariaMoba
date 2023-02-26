﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Osteo;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace TerrariaMoba.Abilities.Osteo {
    public class RiteOfCursedBlades : Ability {
        public RiteOfCursedBlades(Player player) : base(player, "Rite of Cursed Blades", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoUltimateOne").Value; }

        private int Timer;
        
        public override void OnCast() {
            IsActive = true;
            Timer = 0;
        }

        public override void WhileActive() {
            Timer++;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active && plr.team != User.team) {
                    if (!plr.dead) {
                        double displace = Timer;
                        displace -= 34;

                        if (displace < 100) {
                            Dust dust = Dust.NewDustPerfect(new Vector2(plr.Center.X - 1, (float)(displace - 80 + Main.player[i].Center.Y)),
                                59, Vector2.Zero, 1, default, 1.6f);
                            dust.noGravity = true;
                        }
                    }
                }
            }

            if (Timer > 180) {
                TimeOut();
            }

            /*
            Timer++;
            for (int i = 0; i < Main.maxPlayers; i++) {
                if (Main.player[i] != null && Main.player[i].active) {
                    if (Main.player[i].team != User.team) {
                        var modPlayer = Main.player[i].GetModPlayer<MobaPlayer>();
                        if (!Main.player[i].dead) {
                            modPlayer.ultTimer = Timer;

                            double displace = 1f; //OsteoEffects.GetSwordPosition(Timer);
                            float xPosition = Main.player[i].Center.X - 1; //Aligning

                            displace -= 34;

                            if (displace < 100) {
                                Dust dust = Dust.NewDustPerfect(new Vector2(xPosition, (float) (displace - 80 + Main.player[i].Center.Y)), 59, Vector2.Zero, 1, default(Color), 1.6f); //Bobbing
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
            */
        }

        public override void TimeOut() {
            IsActive = false;
            foreach (Player plr in Main.player) {
                if (plr.team != User.team && !plr.dead) {
                    plr.GetModPlayer<MobaPlayer>().TakePvpDamage(300, 300, 0, User.whoAmI, true);
                    StatusEffectManager.AddEffect(plr, new MucormycosisEffect(Mucormycosis.MUCOR_SPORE_DAMAGE, Mucormycosis.MUCOR_SPORE_DURATION, 
                        Mucormycosis.DEBUFF_DURATION, true, User.whoAmI));
                    SoundEngine.PlaySound(SoundID.Item105, plr.position);
                }
            }
            
            /*
            for (int i = 0; i < Main.maxPlayers; i++) {
                if (Main.player[i] != null && Main.player[i].active) {
                    if (Main.player[i].team != User.team) {
                        var modPlayer = Main.player[i].GetModPlayer<MobaPlayer>();
                        if (!Main.player[i].dead) {
                            //modPlayer.DamageOverride((int)User.GetModPlayer<MobaPlayer>().OsteoStats.U1Dmg.Value, Main.player[i], User.whoAmI, true);
                            SoundEngine.PlaySound(SoundID.Item71, Main.player[i].position);
                        }
                        modPlayer.ultTimer = -1;
                    }
                }
            }
            */
        }
    }
}