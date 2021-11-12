using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Marie {
    public class SurgingVitality : Ability {
        public SurgingVitality(Player player) : base(player, "Surging Vitality", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieAbilityTwo").Value; }

        public const int SURGE_HEAL = 369;
        public const int SURGE_CASTTIME = 90;
        public const float SURGE_RANGE = 150f;

        public int Timer = 0;

        public override void OnCast() {
            Timer = SURGE_CASTTIME;
            IsActive = true;
        }

        public override void WhileActive() {
            Timer--;
            for (int d = 0; d < 6; d++) {
                Dust.NewDust(User.position, User.width, User.height, DustID.Water_GlowingMushroom, 0f, 0f, 150, default(Color), 1.5f);
            }
            if (Timer == 0) {
                TimeOut();
            }
        }

        public override void TimeOut() {
            IsActive = false;
            float closestDist  = float.MaxValue;
            int closestPlayerID = -1;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (plr.Center - User.Center).Length();
                if (plr.active && plr.team == User.team && dist <= SURGE_RANGE && i != User.whoAmI) {
                    closestDist = dist < closestDist ? dist : closestDist;
                    closestPlayerID = i;
                }
            }
            
            if (closestPlayerID != -1) {
                Player plr = Main.player[closestPlayerID];
                plr.statLife += SURGE_HEAL;
                CombatText.NewText(plr.Hitbox, Color.CornflowerBlue, SURGE_HEAL, true);
                SoundEngine.PlaySound(SoundID.Item4, plr.Center);
                for (int d = 0; d < 30; d++) {
                    Dust.NewDust(plr.position, plr.width, plr.height, DustID.Water_GlowingMushroom, 0f, 0f, 150, default(Color), 1.5f);
                }
            } else {
                Main.NewText("Could not find a player in range to heal!");
            }
        }
    }
}