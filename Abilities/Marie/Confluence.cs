using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Marie;

namespace TerrariaMoba.Abilities.Marie {
    public class Confluence : Ability {
        public Confluence(Player player) : base(player, "Confluence", 60, 0, AbilityType.Active) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieAbilityThree").Value; } //not trait anymore just texture name

        public const float CONF_RANGE = 150f;
        public const int BUFF_DURATION = 150;
        
        private int alliesInRange;
        
        public override void OnCast() {
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                if (plr.active) {
                    float dist = (plr.Center - User.Center).Length();
                    if (plr.team == User.team && dist <= CONF_RANGE && i != User.whoAmI) {
                        //TODO - Reduce cooldowns of ally non-ultimate abilities.
                        /*
                         * foreach ability plr has
                         * if that ability is on cd
                         * cooldowntimer -= basecooldown * confluencemod
                         */
                        for (int d = 0; d < 40; d++) {
                            Dust.NewDust(plr.position, plr.width, plr.height, DustID.Water_Snow, 0f, 0f, 150, default(Color), 1.5f);
                        }
                        alliesInRange++;
                    }
                }
            }

            if (alliesInRange > 0) {
                StatusEffectManager.AddEffect(User, new ConfluenceEffect(alliesInRange, BUFF_DURATION, true));
            }
        }
    }
}