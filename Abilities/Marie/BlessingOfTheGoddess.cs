using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Abilities.Marie {
    public class BlessingOfTheGoddess : Ability {
        public BlessingOfTheGoddess() : base("Blessing of the Goddess", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieUltimateOne").Value; }

        public const float BOTG_RANGE = 150f;
        
        private int Timer;
        
        public override void OnCast() {
            Timer = 300;
            IsActive = true;
        }

        public override void WhileActive() {
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active) {
                    float dist = (plr.Center - User.Center).Length();
                    if (plr.team == User.team && dist <= BOTG_RANGE) {
                        //StatusEffectManager.AddEffect(plr, new GoddessBlessingEffect());
                        //TODO - Work out permanent buffs given a condition being met
                    }
                }
            }
            Timer--;
        }
    }
}