using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Marie;

namespace TerrariaMoba.Abilities.Marie {
    public class Confluence : Ability {
        public Confluence(Player player) : base(player, "Confluence", 600, 50, AbilityType.Active) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieAbilityThree").Value; }

        public const float CONF_RANGE = 300f;
        public const float CONF_MAGNITUDE = 0.25f;
        public const int CONF_DURATION = 300;

        private int timer;
        
        public override void OnCast() {
            IsActive= true;
            timer = CONF_DURATION;
        }

        public override void WhileActive() {
            if (timer == 0) {
                TimeOut();
            }

            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active) {
                    float dist = (plr.Center - User.Center).Length();
                    if (plr.team == User.team && dist <= CONF_RANGE) {
                        //plr.addeffect confluence +movespeed
                    } else if (plr.team != User.team && dist <= CONF_RANGE) {
                        //plr.addeffect confluence -movespeed
                    }
                }
            }

            timer--;
        }

        public override void TimeOut() {
            IsActive= false;
            CooldownTimer = BaseCooldown;
        }
    }
}